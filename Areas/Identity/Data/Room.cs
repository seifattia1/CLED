using CLED.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CLEDUser Admin { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
