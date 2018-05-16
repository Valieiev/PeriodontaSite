namespace PeriodontalSite1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
          
            DropPrimaryKey("dbo.Appointments");
            DropColumn("dbo.Appointments", "AppointmentId");
            AddColumn("dbo.Appointments", "AppointmentsId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Appointments", "AppointmentsId");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "AppointmentId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Appointments");
            DropColumn("dbo.Appointments", "AppointmentsId");
            AddPrimaryKey("dbo.Appointments", "AppointmentId");
        }
    }
}
