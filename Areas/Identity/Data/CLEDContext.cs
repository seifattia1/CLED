using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CLED.Areas.Admin.ViewModels;
using CLED.Areas.Identity.Data;
using CLED.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CLED.Data
{
    public class CLEDContext : IdentityDbContext<CLEDUser>
    {
        public CLEDContext(DbContextOptions<CLEDContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Facture> Factures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           // builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           
        }
        public DbSet<CLED.Areas.Admin.ViewModels.AdministrationCreateRoleViewModel> AdministrationCreateRoleViewModel { get; set; }
        public DbSet<CLED.Areas.Admin.ViewModels.FactureViewModel> FactureViewModel { get; set; }
    }
}
