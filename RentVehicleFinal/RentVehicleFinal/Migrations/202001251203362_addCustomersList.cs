namespace RentVehicleFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCustomersList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deals", "CustomerId", "dbo.Customers");
            AddColumn("dbo.Customers", "Deal_Id", c => c.Int());
            AddColumn("dbo.Deals", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Customers", "Deal_Id");
            CreateIndex("dbo.Deals", "Customer_Id");
            AddForeignKey("dbo.Customers", "Deal_Id", "dbo.Deals", "Id");
            AddForeignKey("dbo.Deals", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deals", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "Deal_Id", "dbo.Deals");
            DropIndex("dbo.Deals", new[] { "Customer_Id" });
            DropIndex("dbo.Customers", new[] { "Deal_Id" });
            DropColumn("dbo.Deals", "Customer_Id");
            DropColumn("dbo.Customers", "Deal_Id");
            AddForeignKey("dbo.Deals", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
