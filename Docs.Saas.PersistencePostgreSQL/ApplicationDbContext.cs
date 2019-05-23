using Docs.Saas.Model.Auth;
using Docs.Saas.Model.Domaim;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docs.Saas.PersistencePostgreSQL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                    : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserApplicationUser>().HasKey(x => new { x.RevEmpId, x.UserId });

            builder.Entity<ApplicationUser>().Property(x => x.Baja).HasDefaultValue(true);

            var cascadeFKs = builder.Model
                           .G­etEntityTypes()
                           .SelectMany(t => t.GetForeignKeys())
                           .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }



            base.OnModelCreating(builder);
        }

        public DbSet<Licencia> Licencias { get; set; }
        public DbSet<Primium> Primiums { get; set; }
        public DbSet<UserApplicationUser> UserAplicationUsers { get; set; }
        public DbSet<LicenciaUser> LicenciasUsers { get; set; }
        public DbSet<PrimiumUser> PrimiumUsers { get; set; }
    }
}
