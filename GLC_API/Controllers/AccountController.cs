using AutoMapper;
using GLC.Core.ExtendUser;
using GLC.Core.Helper;
using GLC.Core.IUnitOfWork;
using GLC.Core.Resources;
using GLC.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GLC_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IUnitOfWork unitOfWork,
        IMapper mapper
        )

    {
      this.userManager = userManager;
      this.roleManager = roleManager;
      this.configuration = configuration;
      this._unitOfWork = unitOfWork;
      this._mapper = mapper;

    }


    #region JWT
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] SignInVM model)
    {
      var user = await userManager.FindByEmailAsync(model.Email);
      if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
      {
        var userRoles = await userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

        foreach (var userRole in userRoles)
        {
          authClaims.Add(new Claim(ClaimTypes.Role, userRole));
          if (userRole == "Student")
            authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.StudentId.ToString()));
          if (userRole == "Teacher")
            authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.TeacherId.ToString()));
                }

        var token = GetToken(authClaims);

        return Ok(new
        {
          token = new JwtSecurityTokenHandler().WriteToken(token),
          expiration = token.ValidTo
        });
      }
      return Unauthorized();
    }

    [HttpPost]
    [Route("registerStudent")]
    public async Task<IActionResult> RegisterStudent([FromBody] SignUpVM model)
    {
      var userExists = await userManager.FindByNameAsync(model.Email);
      if (userExists != null)
        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User already exists!" });

      ApplicationUser user = new()
      {
        Email = model.Email,
        UserName = model.UserName
      };
      var result = await userManager.CreateAsync(user, model.Password);
      if (!result.Succeeded)
        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });
      if (await roleManager.RoleExistsAsync("Student"))
      {
        await userManager.AddToRoleAsync(user, "Student");
      }
      else
      {
        await roleManager.CreateAsync(new IdentityRole("Student"));
        await userManager.AddToRoleAsync(user, "Student");
      }

      //Mohamed-Nader.
      // I Added This line to add the rest of data into student table. 
      // its two round trip.
      // It can be solvede by trigger-later.
      var student = _mapper.Map<SignUpVM, StudentResource>(model);
      await _unitOfWork.Students.AddAsync(student);
      await _unitOfWork.CompleteAsync();
      return Ok(new ApiResponse { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("registerTeacher")]
    public async Task<IActionResult> RegisterStTeacher([FromBody] SignUpVM model)
    {
      var userExists = await userManager.FindByNameAsync(model.Email);
      if (userExists != null)
        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User already exists!" });

      ApplicationUser user = new()
      {
        Email = model.Email,
        UserName = model.UserName
      };
      var result = await userManager.CreateAsync(user, model.Password);
      if (!result.Succeeded)
        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });
      if (await roleManager.RoleExistsAsync("Teacher"))
      {
        await userManager.AddToRoleAsync(user, "Teacher");
      }
      else
      {
        await roleManager.CreateAsync(new IdentityRole("Teacher"));
        await userManager.AddToRoleAsync(user, "Teacher");
      }


      return Ok(new ApiResponse { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] SignUpVM model)
    {
      var userExists = await userManager.FindByNameAsync(model.Email);
      if (userExists != null)
        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User already exists!" });

      ApplicationUser user = new()
      {
        Email = model.Email,
        UserName = model.UserName
      };
      var result = await userManager.CreateAsync(user, model.Password);
      if (!result.Succeeded)
        return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });


      if (await roleManager.RoleExistsAsync("Admin"))
      {
        await userManager.AddToRoleAsync(user, "Admin");
      }
      else
      {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
        await userManager.AddToRoleAsync(user, "Admin");
      }

      return Ok(new ApiResponse { Status = "Success", Message = "User created successfully!" });
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
      var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

      var token = new JwtSecurityToken(
          issuer: configuration["JWT:ValidIssuer"],
          audience: configuration["JWT:ValidAudience"],
          expires: DateTime.Now.AddHours(3),
          claims: authClaims,
          signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
          );

      return token;
    }

    #endregion

    #region ForgetPass
    [HttpPost("ForgetPasswowrd")]
    public async Task<IActionResult> ForgetPassword(ForgetPassowordVM model)
    {
      if (ModelState.IsValid)
      {
        // Get user by email
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
          // generate token
          var Token = await userManager.GeneratePasswordResetTokenAsync(user);
          var EncodedToken = Encoding.UTF8.GetBytes(Token);
          var ValidToken = WebEncoders.Base64UrlEncode(EncodedToken);

          var PasswordResetLink = $"https://localhost:7027/ResetPassword?email={model.Email}&token={ValidToken}";

          MailSender.SendMail(new MailVM() { Title = "Password Reset - Click the link below", Message = PasswordResetLink, Mail = user.Email });
          return Ok(PasswordResetLink);
        }
        return BadRequest("NO user by this email");
      }
      ModelState.AddModelError("", "Mail not sent");
      return BadRequest(ModelState);
    }
    #endregion

    #region ResetPassowrd
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassowrd(ResetPasswordVM model)
    {
      var user = await userManager.FindByEmailAsync(model.Email);
      if (user != null)
      {
        var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
        string normalToken = Encoding.UTF8.GetString(decodedToken);
        var Result = await userManager.ResetPasswordAsync(user, normalToken, model.Password);

        if (Result.Succeeded)
        {
          return Accepted("Password reset successfully");
        }
        foreach (var item in Result.Errors)
        {
          Problem(item.Description);
        }
      }
      return Problem();
    }

    #endregion






  }
}




