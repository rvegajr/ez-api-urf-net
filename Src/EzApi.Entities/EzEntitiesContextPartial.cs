using System.Data.Entity;
using Repository.Pattern.Ef6;
using Ez.Common;
using System.Data.Entity.Migrations;

namespace Ez.Entities.Models
{
    public partial class EzEntities : DataContext
    {
        public EzEntities() : base(AppSettings.Instance.ConnectionString) {
            Database.SetInitializer<EzEntities>(null);
        }
        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new System.Exception("Code first changes are not allowed.");
        }
        */
    }

    internal sealed class Configuration : DbMigrationsConfiguration<EzEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}


