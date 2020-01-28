namespace RentVehicleFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAvailable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "Available", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "Available");
        }
    }
}
