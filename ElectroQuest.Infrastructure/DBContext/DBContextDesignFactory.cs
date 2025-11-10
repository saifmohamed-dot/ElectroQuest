using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElectroQuest.Infrastructure.DBContext
{
    // this is the class that the CLI tool will use to create ElectroDbcontext at the design time .

    public class ElectroQuestDbContextDesignFactory : IDesignTimeDbContextFactory<ElectroQuestDbContext>
    {
        public ElectroQuestDbContext CreateDbContext(string[] args)
        {
           var optionbuilder = new DbContextOptionsBuilder<ElectroQuestDbContext>();
            // will pass the connection string hard coded 
            // just for migrations purpose.
            // but in the runtime we will depend on the appsettings.json
            optionbuilder.UseSqlServer("Data Source=.;Initial Catalog=ElectroDB;Integrated Security=True;TrustServerCertificate=True");
            return new ElectroQuestDbContext(optionbuilder.Options);
        }
        public ElectroQuestDbContext CreateDbContext(string connectoinString)
        {
            var optionbuilder = new DbContextOptionsBuilder<ElectroQuestDbContext>();
            optionbuilder.UseSqlServer(connectoinString);
            return new ElectroQuestDbContext(optionbuilder.Options);
        }
    }
}
