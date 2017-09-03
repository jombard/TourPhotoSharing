namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStarredImagesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StarredImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image_Id = c.Guid(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Image_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StarredImages", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StarredImages", "Image_Id", "dbo.Images");
            DropIndex("dbo.StarredImages", new[] { "User_Id" });
            DropIndex("dbo.StarredImages", new[] { "Image_Id" });
            DropTable("dbo.StarredImages");
        }
    }
}
