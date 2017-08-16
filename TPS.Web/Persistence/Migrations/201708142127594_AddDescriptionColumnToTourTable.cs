namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionColumnToTourTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "Description");
        }
    }
}
