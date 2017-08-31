namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIdentityFromAuditId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Audits");
            AlterColumn("dbo.Audits", "AuditId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Audits", "AuditId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Audits");
            AlterColumn("dbo.Audits", "AuditId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Audits", "AuditId");
        }
    }
}
