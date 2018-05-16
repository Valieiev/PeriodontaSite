namespace PeriodontalSite1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db : DbMigration
    {
        public override void Up()
        {
            
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientsId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.PatientsId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        Patronymic = c.String(),
                        Sex = c.String(),
                        TypeUser = c.Int(nullable: false),
                        Address = c.String(),
                        Birth = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
           
            
          
            CreateTable(
                "dbo.TypeServices",
                c => new
                    {
                        TypeServicesId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TypeServicesId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitsId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.UnitsId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
              "dbo.Services",
              c => new
              {
                  ServicesId = c.Int(nullable: false, identity: true),
                  Name = c.String(),
                  Description = c.String(),
                  TypeId = c.Int(nullable: false),
                  UnitId = c.Int(nullable: false),
              })
              .PrimaryKey(t => t.ServicesId)
              .ForeignKey("dbo.TypeServices", t => t.TypeId)
              .ForeignKey("dbo.Units", t => t.UnitId)
              .Index(t => t.TypeId)
              .Index(t => t.UnitId);


            CreateTable(
               "dbo.Prices",
               c => new
               {
                   PriceId = c.Int(nullable: false, identity: true),
                   Value = c.Double(nullable: false),
                   FromDate = c.DateTime(nullable: false),
                   ToDate = c.DateTime(nullable: false),
                   ServiceId = c.Int(nullable: false),
               })
               .PrimaryKey(t => t.PriceId)
               .ForeignKey("dbo.Services", t => t.ServiceId)
               .Index(t => t.ServiceId);

            CreateTable(
                "dbo.Appointments",
                c => new
                {
                    AppointmentsId = c.Int(nullable: false, identity: true),
                    VisitDate = c.DateTime(nullable: false),
                    AppointmentStatus = c.Int(nullable: false),
                    UserId = c.String(maxLength: 128),
                    PatientId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.AppointmentsId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PatientId)
                .Index(t => t.UserId);
                
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Prices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Services", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Services", "TypeId", "dbo.TypeServices");
            DropForeignKey("dbo.Appointments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Services", new[] { "UnitId" });
            DropIndex("dbo.Services", new[] { "TypeId" });
            DropIndex("dbo.Prices", new[] { "ServiceId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropIndex("dbo.Appointments", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Units");
            DropTable("dbo.TypeServices");
            DropTable("dbo.Services");
            DropTable("dbo.Prices");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Patients");
            DropTable("dbo.Appointments");
        }
    }
}
