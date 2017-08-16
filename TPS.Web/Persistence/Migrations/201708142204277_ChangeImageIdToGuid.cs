namespace TPS.Web.Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeImageIdToGuid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TourImages", "Image_Id", "dbo.Images");
            DropIndex("dbo.TourImages", new[] { "Image_Id" });
            DropPrimaryKey("dbo.Images");
            DropColumn("dbo.Images", "Id");
            AddColumn("dbo.Images", "Id", c => c.Guid(nullable: false, identity: true));
            DropColumn("dbo.TourImages", "Image_Id");
            AddColumn("dbo.TourImages", "Image_Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Images", "Id");
            CreateIndex("dbo.TourImages", "Image_Id");
            AddForeignKey("dbo.TourImages", "Image_Id", "dbo.Images", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourImages", "Image_Id", "dbo.Images");
            DropIndex("dbo.TourImages", new[] { "Image_Id" });
            DropPrimaryKey("dbo.Images");
            DropColumn("dbo.TourImages", "Image_Id");
            AddColumn("dbo.TourImages", "Image_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Images", "Id");
            AddColumn("dbo.Images", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Images", "Id");
            CreateIndex("dbo.TourImages", "Image_Id");
            AddForeignKey("dbo.TourImages", "Image_Id", "dbo.Images", "Id", cascadeDelete: true);
        }
    }
}
