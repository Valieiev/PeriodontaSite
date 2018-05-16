namespace PeriodontalSite1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeCount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppointmentResult", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppointmentResult", "Count", c => c.String());
        }
    }
}
