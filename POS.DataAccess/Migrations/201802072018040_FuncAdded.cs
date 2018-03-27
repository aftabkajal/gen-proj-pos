namespace POS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FuncAdded : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Total");
            DropColumn("dbo.Sales", "TotalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "TotalPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Orders", "Total", c => c.Double(nullable: false));
        }
    }
}
