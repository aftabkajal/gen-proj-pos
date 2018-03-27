namespace POS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAllModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.String(),
                        Name = c.String(),
                        Brand = c.String(),
                        Color = c.String(),
                        Size = c.String(),
                        Quantity = c.Long(nullable: false),
                        UnitBuyingPrice = c.Double(nullable: false),
                        UnitSellingPrice = c.Double(nullable: false),
                        EntryDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PossibleDiscountPercentage = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        Address = c.String(),
                        PurcheseDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TotalPrice = c.Double(nullable: false),
                        AdvancePayment = c.Double(nullable: false),
                        DuePayment = c.Double(nullable: false),
                        Issuer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Issuer_Id)
                .Index(t => t.Issuer_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Product_Id = c.Int(),
                        Sale_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Sales", t => t.Sale_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Sale_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Sale_Id", "dbo.Sales");
            DropForeignKey("dbo.Orders", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Sales", "Issuer_Id", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "Sale_Id" });
            DropIndex("dbo.Orders", new[] { "Product_Id" });
            DropIndex("dbo.Sales", new[] { "Issuer_Id" });
            DropTable("dbo.Orders");
            DropTable("dbo.Users");
            DropTable("dbo.Sales");
            DropTable("dbo.Products");
        }
    }
}
