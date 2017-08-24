namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQueryColumnToImageTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Query", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Query");
        }
    }
}
