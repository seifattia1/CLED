using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Areas.Admin.ViewModels
{
    public class FactureViewModel
    {
        [Key]
        [PersonalData]
        public int FactureId { get; set; }


        public string userName { get; set; }
        public int Price { get; set; }

        public DateTime FactureDate { get; set; }
        public string FactureStatus { get; set; }
    }
}
