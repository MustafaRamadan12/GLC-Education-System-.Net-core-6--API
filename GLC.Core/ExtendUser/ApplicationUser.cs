using GLC.Cores.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLC.Core.ExtendUser
{
    public class ApplicationUser:IdentityUser
    {
        [ForeignKey("Student")]
        public Guid? StudentId { get; set; }
        public Student? Student { get; set; }

        [ForeignKey("Teacher")]
        public Guid? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
