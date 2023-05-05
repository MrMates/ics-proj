using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Factories
{
    public class DbContextSqlLiteFactory : IDbContextFactory<DatabaseContext>
    {
        private readonly DbContextOptionsBuilder<DatabaseContext> _contextOptionsBuilder = new();
        private readonly bool _seedTestingData;
        public DbContextSqlLiteFactory(string databaseName, bool seedTestingData = false)
        {
            _seedTestingData = seedTestingData;
            _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");
#if DEBUG
            _contextOptionsBuilder.LogTo(message => Debug.WriteLine(message));
#endif
        }

        public DatabaseContext CreateDbContext()
            => new(_contextOptionsBuilder.Options, _seedTestingData);
    }
}
