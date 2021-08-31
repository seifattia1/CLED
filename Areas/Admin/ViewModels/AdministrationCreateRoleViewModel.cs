using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Areas.Admin.ViewModels
{
    [NotMapped]
    public class AdministrationCreateRoleViewModel
    {
        [Required,StringLength(256)]
        [Display(Name ="Role")]
        public string Name { get; set; }
    }
}
