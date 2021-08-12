using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CLED.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CLEDUser class
    public class CLEDUser : IdentityUser
    {
        
        [Required]
        [PersonalData]
        [Column(TypeName =("nvarchar(100)"))]
        [Display(Name="Nom")]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        [Display(Name = "Prenom")]
        [Column(TypeName = ("nvarchar(100)"))]
        public string LastName { get; set; }


        [PersonalData]
        [Display(Name = "Account Type")]
        [Column(TypeName = ("nvarchar(100)"))]
        public string AccountType { get; set; }

    }
}
