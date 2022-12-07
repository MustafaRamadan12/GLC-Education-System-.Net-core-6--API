using GLC.Core.Helper;
using GLC.Core.ViewModels;
using GLC.Cores.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace GLC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UsersRolesManagmentController : ControllerBase
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public UsersRolesManagmentController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;

        }

        [HttpGet]
        public async Task<IActionResult> AssignUserToRole()
        {
            List<UsersRolesVM> usersRoles = new List<UsersRolesVM>();
            var userRole = new UsersRolesVM();
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            foreach (var user in users)
            {
                userRole.UserName = user.UserName;
                foreach (var role in roles)
                {
                    userRole.RoleName = role.Name;
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        usersRoles.Add(userRole);
                    }
                }
            }

            if (usersRoles.Count() >= 0)
            {
                return Ok(userRole);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("AssignUserToRole")]
        public async Task<IActionResult> AssignUserToRole(UsersRolesVM userRole)
        {
            var user = await userManager.FindByIdAsync(userRole.UserId);
            var role = await roleManager.FindByNameAsync(userRole.RoleName);
            if (userRole == null)
            {
                return Problem("Role name and user name can't be null");
            }
            else
            {
                if (!await userManager.IsInRoleAsync(user, role.Name))
                {
                    var result = await userManager.AddToRoleAsync(user, userRole.RoleName);
                    if (result.Succeeded)
                    {
                        return Created("done", userRole);
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            return Problem(item.Description);
                        }
                    }
                }
                return Problem("User already assigned to this role");
            }
        }
        [HttpDelete("RemoveUserToRole")]
        public async Task<IActionResult> RemoveUserToRole(UsersRolesVM userRole)
        {
            var user = await userManager.FindByNameAsync(userRole.UserName);

            var role = await roleManager.FindByNameAsync(userRole.RoleName);

            if (user == null || role == null)
            {
                return Problem("Role name and user name can't be null");
            }
            else
            {

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    var result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    if (result.Succeeded)
                    {
                        return Ok("User deleted from the role");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            return Problem(item.Description);
                        }
                    }
                }
                return Problem("User didn't assign for this role");
            }
        }
    }
}
