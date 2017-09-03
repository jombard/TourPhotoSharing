namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropThumbnailColumnFromImage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Images", "Thumbnail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Thumbnail", c => c.Binary());
        }
    }
}
