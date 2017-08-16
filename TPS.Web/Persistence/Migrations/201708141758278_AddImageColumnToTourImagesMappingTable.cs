namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageColumnToTourImagesMappingTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourImages", "Image_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.TourImages", "Image_Id");
            AddForeignKey("dbo.TourImages", "Image_Id", "dbo.Images", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourImages", "Image_Id", "dbo.Images");
            DropIndex("dbo.TourImages", new[] { "Image_Id" });
            DropColumn("dbo.TourImages", "Image_Id");
        }
    }
}
