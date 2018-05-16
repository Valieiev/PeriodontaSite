
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.Models
{
    public class ServicesContext : DbContext
    {


        public DbSet<Services> Services { get; set; }
        public DbSet<TypeServices> TypeServices { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<Prices> Prices { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Appointments> Appointments { get; set; }


        public ServicesContext() : base("IdentityDb")
        { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure primary key for Services
            modelBuilder.Entity<Services>()
             .HasRequired<TypeServices>(s => s.Type)
             .WithMany(p => p.Services)
            .HasForeignKey<int>(s => s.TypeId);


            modelBuilder.Entity<Services>()
                .HasRequired<Units>(s => s.Unit)
                .WithMany(d => d.Services)
                .HasForeignKey<int>(s => s.UnitId);

            modelBuilder.Entity<Prices>()
                .HasRequired<Services>(s => s.Services)
                .WithMany(d => d.Prices)
                .HasForeignKey<int>(s => s.ServiceId);

            modelBuilder.Entity<Appointments>()
                .HasRequired<Patients>(s => s.Patient)
                .WithMany(d => d.Appointments)
                .HasForeignKey<int>(s => s.AppointmentsId);


        }
    }
}