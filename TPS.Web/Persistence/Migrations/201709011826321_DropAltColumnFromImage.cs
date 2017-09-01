namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropAltColumnFromImage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Images", "AltText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "AltText", c => c.String());
        }
    }
}
