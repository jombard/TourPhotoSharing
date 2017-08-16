namespace TPS.Web.Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeTourIdToGuid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TourImages", "Tour_Id", "dbo.Tours");
            DropIndex("dbo.TourImages", new[] { "Tour_Id" });
            DropPrimaryKey("dbo.Tours");
            DropColumn("dbo.TourImages", "Tour_Id");
            AddColumn("dbo.TourImages", "Tour_Id", c => c.Guid(nullable: false));
            DropColumn("dbo.Tours", "Id");
            AddColumn("dbo.Tours", "Id", c => c.Guid(nullable: false, identity: true ));
            AddPrimaryKey("dbo.Tours", "Id");
            CreateIndex("dbo.TourImages", "Tour_Id");
            AddForeignKey("dbo.TourImages", "Tour_Id", "dbo.Tours", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourImages", "Tour_Id", "dbo.Tours");
            DropIndex("dbo.TourImages", new[] { "Tour_Id" });
            DropPrimaryKey("dbo.Tours");
            DropColumn("dbo.Tours", "Id");
            AddColumn("dbo.Tours", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.TourImages", "Tour_Id");
            AddColumn("dbo.TourImages", "Tour_Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Tours", "Id");
            CreateIndex("dbo.TourImages", "Tour_Id");
            AddForeignKey("dbo.TourImages", "Tour_Id", "dbo.Tours", "Id", cascadeDelete: true);
        }
    }
}
