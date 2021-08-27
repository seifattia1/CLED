using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CLED.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CLED.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CLEDUser class
    public class CLEDUser : IdentityUser
    {
       
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Message> Messages { get; set; }
        

    }
}
