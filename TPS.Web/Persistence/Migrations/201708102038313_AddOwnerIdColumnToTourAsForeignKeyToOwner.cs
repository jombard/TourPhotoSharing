using System.Data.Entity.Migrations;

namespace TPS.Web.Persistence.Migrations
{
    public partial class AddOwnerIdColumnToTourAsForeignKeyToOwner : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tours", name: "Owner_Id", newName: "OwnerId");
            RenameIndex(table: "dbo.Tours", name: "IX_Owner_Id", newName: "IX_OwnerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Tours", name: "IX_OwnerId", newName: "IX_Owner_Id");
            RenameColumn(table: "dbo.Tours", name: "OwnerId", newName: "Owner_Id");
        }
    }
}
