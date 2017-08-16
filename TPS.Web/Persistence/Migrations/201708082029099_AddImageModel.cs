using System.Data.Entity.Migrations;

namespace TPS.Web.Persistence.Migrations
{
    public partial class AddImageModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        AltText = c.String(),
                        Caption = c.String(),
                        ImageUrl = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Images");
        }
    }
}
