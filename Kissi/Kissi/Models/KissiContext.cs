using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Kissi.Models
{
    public class KissiContext: DbContext
    {
        public KissiContext():base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Department> Departments { get; set; }

        public System.Data.Entity.DbSet<Kissi.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<Kissi.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<Kissi.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<Kissi.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Kissi.Models.Tax> Taxes { get; set; }

        public System.Data.Entity.DbSet<Kissi.Models.Product> Products { get; set; }
    }
}