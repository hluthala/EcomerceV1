using System.Data.Entity;

namespace Kissi.Models
{
    public class KissiContext: DbContext
    {
        public KissiContext():base("DefaultConnection")
        {

        }

        public DbSet<Department> Departments { get; set; }

        public System.Data.Entity.DbSet<Kissi.Models.City> Cities { get; set; }
    }
}