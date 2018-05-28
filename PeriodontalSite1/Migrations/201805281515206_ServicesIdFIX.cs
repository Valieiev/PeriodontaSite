namespace PeriodontalSite1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServicesIdFIX : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "UnitId", "dbo.Units");
            RenameColumn(table: "dbo.Prices", name: "ServiceId", newName: "ServicesId");
            RenameIndex(table: "dbo.Prices", name: "IX_ServiceId", newName: "IX_ServicesId");
            AlterColumn("dbo.Prices", "ToDate", c => c.DateTime());
            AddForeignKey("dbo.Services", "UnitId", "dbo.Units", "UnitsId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "UnitId", "dbo.Units");
            AlterColumn("dbo.Prices", "ToDate", c => c.DateTime(nullable: false));
            RenameIndex(table: "dbo.Prices", name: "IX_ServicesId", newName: "IX_ServiceId");
            RenameColumn(table: "dbo.Prices", name: "ServicesId", newName: "ServiceId");
            AddForeignKey("dbo.Services", "UnitId", "dbo.Units", "UnitsId");
        }
    }
}
