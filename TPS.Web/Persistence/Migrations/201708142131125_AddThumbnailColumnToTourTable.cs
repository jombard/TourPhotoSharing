namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddThumbnailColumnToTourTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "Thumbnail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "Thumbnail");
        }
    }
}
