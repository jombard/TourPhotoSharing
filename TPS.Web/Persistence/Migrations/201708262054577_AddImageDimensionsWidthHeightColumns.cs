namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageDimensionsWidthHeightColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Width", c => c.Int());
            AddColumn("dbo.Images", "Height", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Height");
            DropColumn("dbo.Images", "Width");
        }
    }
}
