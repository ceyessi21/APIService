using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ConvertSqlServerToSQLite.Models
{
    public partial class liteContext : DbContext
    {

        private bool _disposed;
        public liteContext(DbContextOptions<liteContext> options) : base(options)
        {

        }
        public  DbSet<Personne> personne { get; set; }

        public override void Dispose()

        {

            Dispose(true);

            GC.SuppressFinalize(this);

        }



        protected void Dispose(bool disposing)

        {

            if (!_disposed)

            {

                if (disposing)

                {

                    Database.GetDbConnection().Close();

                    base.Dispose();

                }

            }

            _disposed = true;

        }

    }


}
