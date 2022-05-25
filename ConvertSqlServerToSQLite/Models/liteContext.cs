using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ConvertSqlServerToSQLite.Models
{
    public partial class liteContext : DbContext
    {
        public liteContext(DbContextOptions<liteContext> options) : base(options)
        {

        }
        public  DbSet<Personne> personne { get; set; }    

    }
}
