using GLC.Cores.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace GLC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [HttpGet("ViewAll")]
        public IActionResult Index()
        {
            List<IdentityRole> roles = roleManager.Roles.ToList();
            if (roles.Count() >= 0)
            {
                return Ok(roles);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
        [HttpPost("CreatRole")]
        public async Task<IActionResult> Add(IdentityRole role)
        {
            if (role == null)
            {
                return Problem("Role Name is required");
            }
            else
            {
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Created("done", role);
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        Problem(err.Description);
                    }
                }

                return Ok(role);
            }
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(IdentityRole model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return NotFound();
            }
            if (model == null)
            {
                return Problem("Role Name is required");
            }
           
                role.Name = model.Name;
                role.NormalizedName = model.NormalizedName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        return Problem(err.Description);
                    }
                }
                return Ok(role);
            
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok(roleManager.Roles);
            }
            else
            {
                foreach (var err in result.Errors)
                {
                    return Problem(err.Description);
                }
            }
            return Ok(role);
        }
    }
}
