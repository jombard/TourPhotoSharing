namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStarredImageCollectionsForUserAndImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StarredImages",
                c => new
                    {
                        UserRefId = c.String(nullable: false, maxLength: 128),
                        ImageRefId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.ImageRefId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserRefId, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.ImageRefId, cascadeDelete: true)
                .Index(t => t.UserRefId)
                .Index(t => t.ImageRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StarredImages", "ImageRefId", "dbo.Images");
            DropForeignKey("dbo.StarredImages", "UserRefId", "dbo.AspNetUsers");
            DropIndex("dbo.StarredImages", new[] { "ImageRefId" });
            DropIndex("dbo.StarredImages", new[] { "UserRefId" });
            DropTable("dbo.StarredImages");
        }
    }
}
