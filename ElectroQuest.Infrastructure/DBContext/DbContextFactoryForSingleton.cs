
using ElectroQuest.Application.Analytics.Services.Interfaces;
using ElectroQuest.Infrastructure.Analytics.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ElectroQuest.Infrastructure.DBContext
{
    public class DbContextFactoryForSingleton : IDbFactory
    {
        readonly DbSettings _dbSettings;
        public DbContextFactoryForSingleton(IOptions<DbSettings> options)
        {
            _dbSettings = options.Value;
        }
        public DbContext CreateDbContext()
        {
            // we will containerize this later .
            // so we need to read this configuration from the appsettings.json
            var factory = new ElectroQuestDbContextDesignFactory();
            return factory.CreateDbContext(_dbSettings.DefaultConnection);
        }
    }
}
