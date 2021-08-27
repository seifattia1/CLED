using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Areas.Admin.ViewModels
{
    public class AdministrationCreateRoleViewModel:IdentityRole
    {
        [Required]
        [Display(Name ="Role")]
        public string RoleName { get; set; }
    }
}
