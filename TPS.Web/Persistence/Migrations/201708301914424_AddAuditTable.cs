namespace TPS.Web.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        AuditId = c.Guid(nullable: false),
                        UserName = c.String(),
                        IPAddress = c.String(),
                        AreaAccessed = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuditId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Audits");
        }
    }
}
