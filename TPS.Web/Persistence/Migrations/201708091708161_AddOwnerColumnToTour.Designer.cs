// <auto-generated />

using System.CodeDom.Compiler;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace TPS.Web.Persistence.Migrations
{
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class AddOwnerColumnToTour : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddOwnerColumnToTour));
        
        string IMigrationMetadata.Id
        {
            get { return "201708091708161_AddOwnerColumnToTour"; }
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
