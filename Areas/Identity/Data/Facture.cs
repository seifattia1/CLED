using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Areas.Identity.Data
{
    public class Facture 
    {
        [Key]
        [PersonalData]
        public int FactureId { get; set; }

        
        public string userName { get; set; }
        public int Price { get; set; }

        [DataType(DataType.Date)]
        public string FactureDate { get; set; }
        public string FactureStatus { get; set; }

       
    }
}
