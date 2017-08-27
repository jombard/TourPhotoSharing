namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentTableWithForeignKeys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CommenterId = c.String(maxLength: 128),
                        CommentValue = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                        TourId = c.Guid(nullable: false),
                        ParentCommentId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CommenterId)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.CommenterId)
                .Index(t => t.TourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "TourId", "dbo.Tours");
            DropForeignKey("dbo.Comments", "CommenterId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "TourId" });
            DropIndex("dbo.Comments", new[] { "CommenterId" });
            DropTable("dbo.Comments");
        }
    }
}
