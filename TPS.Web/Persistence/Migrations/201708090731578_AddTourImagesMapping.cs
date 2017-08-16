using System.Data.Entity.Migrations;

namespace TPS.Web.Persistence.Migrations
{
    public partial class AddTourImagesMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tour_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.Tour_Id, cascadeDelete: true)
                .Index(t => t.Tour_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourImages", "Tour_Id", "dbo.Tours");
            DropIndex("dbo.TourImages", new[] { "Tour_Id" });
            DropTable("dbo.TourImages");
        }
    }
}
