namespace POS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCustomerToSale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Sales", "Customer_Id");
            AddForeignKey("dbo.Sales", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Sales", new[] { "Customer_Id" });
            DropColumn("dbo.Sales", "Customer_Id");
        }
    }
}
