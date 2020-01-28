namespace RentVehicleFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPlatenoToDeal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deals", "plateno", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deals", "plateno");
        }
    }
}
