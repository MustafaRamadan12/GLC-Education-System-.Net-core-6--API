using GLC.Cores.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GLC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
      

        public UsersController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
           
        }
        #region Index
        [HttpGet("AllUsers")]
        public IActionResult GetAllUsers()
        {
            var data = userManager.Users;
            if (data.Count() >= 0)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion 

        #region ById
        [HttpGet("UserById")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await userManager.FindByIdAsync(id);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();
            }
        }

        #endregion

        #region Edit


        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(IdentityUser model)
        {
           
                var user = await userManager.FindByIdAsync(model.Id);
                if (model == null)
                {
                    return Problem("User name can't be null");
                }
                if (user == null)
                {
                    return NotFound("User not exist");
                }
                else
                {
                    user.UserName = model.UserName;
                    var result = await userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return NoContent(); ;

                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            return Problem(item.Description);
                        }
                    }
                return Ok();
                }
        }

        #endregion

        #region Delete
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> Delete(IdentityUser model)
        {
            var user = await userManager.FindByIdAsync(model.Id);


            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok(userManager.Users);

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return Ok("Deleted");
        }
        #endregion
    }
}
