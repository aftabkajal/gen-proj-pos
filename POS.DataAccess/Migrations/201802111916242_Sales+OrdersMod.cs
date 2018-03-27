namespace POS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesOrdersMod : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Total", c => c.Double(nullable: false));
        }
    }
}
