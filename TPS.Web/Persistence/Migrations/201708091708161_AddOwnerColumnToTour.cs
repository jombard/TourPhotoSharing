using System.Data.Entity.Migrations;

namespace TPS.Web.Persistence.Migrations
{
    public partial class AddOwnerColumnToTour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "Owner_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tours", "Owner_Id");
            AddForeignKey("dbo.Tours", "Owner_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tours", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tours", new[] { "Owner_Id" });
            DropColumn("dbo.Tours", "Owner_Id");
        }
    }
}
