namespace POS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaleMod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "DiscountToken_Id", c => c.Int());
            CreateIndex("dbo.Sales", "DiscountToken_Id");
            AddForeignKey("dbo.Sales", "DiscountToken_Id", "dbo.DiscountTokens", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "DiscountToken_Id", "dbo.DiscountTokens");
            DropIndex("dbo.Sales", new[] { "DiscountToken_Id" });
            DropColumn("dbo.Sales", "DiscountToken_Id");
        }
    }
}
