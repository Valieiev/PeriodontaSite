using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using PeriodontalSite1.Models.Users;
namespace PeriodontalSite1.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Services> Services { get; set; }
        public DbSet<TypeServices> TypeServices { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<Prices> Prices { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<AppointmentResult> AppointmentsResult { get; set; }

        public ApplicationContext() : base("IdentityDb")

        {
            Database.SetInitializer<ApplicationContext>(null);
            Configuration.ProxyCreationEnabled = false;
           Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
         
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //Configure primary key for Services
            modelBuilder.Entity<Services>()
             .HasRequired<TypeServices>(s => s.Types)
             .WithMany(p => p.Services)
            .HasForeignKey<int>(s => s.TypeId);


            modelBuilder.Entity<Services>()
                .HasRequired<Units>(s => s.Units)
                .WithMany(d => d.Services)
                .HasForeignKey<int>(s => s.UnitId)
                .WillCascadeOnDelete(false);
            //Configure primary key for Prices
            modelBuilder.Entity<Prices>()
                .HasRequired<Services>(s => s.Services)
                .WithMany(d => d.Prices)
                .HasForeignKey<int>(s => s.ServiceId);

            modelBuilder.Entity<Appointments>()
                .HasRequired<Patients>(s => s.Patient)
                .WithMany(d => d.Appointments)
                .HasForeignKey<int>(s => s.PatientId);

            modelBuilder.Entity<Appointments>()
               .HasRequired<Patients>(s => s.Patient)
               .WithMany(d => d.Appointments)
               .HasForeignKey<int>(s => s.PatientId);

            modelBuilder.Entity<AppointmentResult>()
               .HasRequired<Appointments>(s => s.Appoitment)
               .WithMany(d => d.Results)
               .HasForeignKey<int>(s => s.AppoitmentId);

            modelBuilder.Entity<AppointmentResult>()
               .HasRequired<Prices>(s => s.Price)
               .WithMany(d => d.Results)
               .HasForeignKey<int>(s => s.PriceId);

            base.OnModelCreating(modelBuilder);

        }

        }
    }