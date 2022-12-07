using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GLC.Core.ViewModels
{
    public class UsersRolesVM
    {
        [Display(Name = "UserId")]
        public string? UserId { get; set; }
        [Display(Name = "User")]
        [Required]
        public string? UserName{ get; set; }
        [Display(Name = "Role")]
        [Required]
        public string? RoleName { get; set; }
        //public ICollection<IdentityUser> Users { get; set; }= new Collection<IdentityUser>();
        //public ICollection<IdentityRole> Roles { get; set; }= new Collection<IdentityRole>();
    }
}
