using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Factories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        private readonly DbContextSqlLiteFactory _dbContextSqlLiteFactory;
        private const string ConnectionString = $"Data Source=Project;Cache=Shared";
        public DesignTimeDbContextFactory()
        {
            _dbContextSqlLiteFactory = new DbContextSqlLiteFactory(ConnectionString);
        }
        public DatabaseContext CreateDbContext(string[] args)
            => _dbContextSqlLiteFactory.CreateDbContext();
    }
}
