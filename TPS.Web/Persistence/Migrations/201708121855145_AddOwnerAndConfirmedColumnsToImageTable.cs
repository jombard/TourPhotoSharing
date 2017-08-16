using System.Data.Entity.Migrations;

namespace TPS.Web.Persistence.Migrations
{
    public partial class AddOwnerAndConfirmedColumnsToImageTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Images", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Images", "OwnerId");
            AddForeignKey("dbo.Images", "OwnerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Images", new[] { "OwnerId" });
            DropColumn("dbo.Images", "OwnerId");
            DropColumn("dbo.Images", "Confirmed");
        }
    }
}
