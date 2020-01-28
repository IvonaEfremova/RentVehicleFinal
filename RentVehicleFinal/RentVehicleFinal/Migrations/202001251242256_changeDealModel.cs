namespace RentVehicleFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDealModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "Deal_Id", "dbo.Deals");
            DropForeignKey("dbo.Deals", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Customers", new[] { "Deal_Id" });
            DropIndex("dbo.Deals", new[] { "CustomerId" });
            DropIndex("dbo.Deals", new[] { "Customer_Id" });
            DropColumn("dbo.Deals", "CustomerId");
            RenameColumn(table: "dbo.Deals", name: "Customer_Id", newName: "CustomerId");
            AlterColumn("dbo.Deals", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Deals", "CustomerId");
            AddForeignKey("dbo.Deals", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            DropColumn("dbo.Customers", "Deal_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Deal_Id", c => c.Int());
            DropForeignKey("dbo.Deals", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Deals", new[] { "CustomerId" });
            AlterColumn("dbo.Deals", "CustomerId", c => c.Int());
            RenameColumn(table: "dbo.Deals", name: "CustomerId", newName: "Customer_Id");
            AddColumn("dbo.Deals", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Deals", "Customer_Id");
            CreateIndex("dbo.Deals", "CustomerId");
            CreateIndex("dbo.Customers", "Deal_Id");
            AddForeignKey("dbo.Deals", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Customers", "Deal_Id", "dbo.Deals", "Id");
        }
    }
}
