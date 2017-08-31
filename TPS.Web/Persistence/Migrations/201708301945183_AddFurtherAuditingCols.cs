namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFurtherAuditingCols : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Audits");
            AddColumn("dbo.Audits", "SessionId", c => c.String());
            AddColumn("dbo.Audits", "Data", c => c.String());
            AlterColumn("dbo.Audits", "AuditId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Audits", "AuditId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Audits");
            AlterColumn("dbo.Audits", "AuditId", c => c.Guid(nullable: false));
            DropColumn("dbo.Audits", "Data");
            DropColumn("dbo.Audits", "SessionId");
            AddPrimaryKey("dbo.Audits", "AuditId");
        }
    }
}
