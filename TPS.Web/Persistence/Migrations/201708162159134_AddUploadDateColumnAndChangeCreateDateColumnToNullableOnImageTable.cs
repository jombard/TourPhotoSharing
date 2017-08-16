namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUploadDateColumnAndChangeCreateDateColumnToNullableOnImageTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "UploadDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Images", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Images", "CreatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Images", "UploadDate");
        }
    }
}
