namespace PeriodontalSite1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppResult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppointmentResult",
                c => new
                    {
                        AppointmentResultId = c.Int(nullable: false, identity: true),
                        Count = c.String(),
                        AppoitmentId = c.Int(nullable: false),
                        PriceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentResultId)
                .ForeignKey("dbo.Appointments", t => t.AppoitmentId)
                .ForeignKey("dbo.Prices", t => t.PriceId)
                .Index(t => t.AppoitmentId)
                .Index(t => t.PriceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppointmentResult", "PriceId", "dbo.Prices");
            DropForeignKey("dbo.AppointmentResult", "AppoitmentId", "dbo.Appointments");
            DropIndex("dbo.AppointmentResult", new[] { "PriceId" });
            DropIndex("dbo.AppointmentResult", new[] { "AppoitmentId" });
            DropTable("dbo.AppointmentResult");
        }
    }
}
