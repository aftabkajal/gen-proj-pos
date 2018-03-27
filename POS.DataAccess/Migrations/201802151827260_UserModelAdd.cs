namespace POS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModelAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ContactNo", c => c.String());
            AddColumn("dbo.Users", "Address", c => c.String());
            AddColumn("dbo.Users", "LegalInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LegalInfo");
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "ContactNo");
        }
    }
}
