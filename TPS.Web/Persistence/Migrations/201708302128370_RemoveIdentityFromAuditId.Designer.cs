// <auto-generated />
namespace TPS.Web.Persistence.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class RemoveIdentityFromAuditId : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(RemoveIdentityFromAuditId));
        
        string IMigrationMetadata.Id
        {
            get { return "201708302128370_RemoveIdentityFromAuditId"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}