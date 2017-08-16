namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLatAndLongColumnsToImageTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Latitude", c => c.Single());
            AddColumn("dbo.Images", "Longitude", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Longitude");
            DropColumn("dbo.Images", "Latitude");
        }
    }
}
