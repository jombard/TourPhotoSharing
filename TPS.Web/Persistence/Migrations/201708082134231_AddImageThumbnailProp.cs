using System.Data.Entity.Migrations;

namespace TPS.Web.Persistence.Migrations
{
    public partial class AddImageThumbnailProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Thumbnail", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Thumbnail");
        }
    }
}
