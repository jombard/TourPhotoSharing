using System.Data.Entity.Migrations;

namespace TPS.Web.Persistence.Migrations
{
    public partial class AddImageMimetypeProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "ImageMimeType");
        }
    }
}
