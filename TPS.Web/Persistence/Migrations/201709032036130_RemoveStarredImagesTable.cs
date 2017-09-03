namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveStarredImagesTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StarredImages", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.StarredImages", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.StarredImages", new[] { "Image_Id" });
            DropIndex("dbo.StarredImages", new[] { "User_Id" });
            DropTable("dbo.StarredImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StarredImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image_Id = c.Guid(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.StarredImages", "User_Id");
            CreateIndex("dbo.StarredImages", "Image_Id");
            AddForeignKey("dbo.StarredImages", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StarredImages", "Image_Id", "dbo.Images", "Id", cascadeDelete: true);
        }
    }
}
