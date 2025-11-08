using ElectroQuest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ElectroQuest.Infrastructure.DBContext
{
    public class ElectroQuestDbContext : DbContext
    {
        public ElectroQuestDbContext(DbContextOptions<ElectroQuestDbContext> options) : base(options) { }
        public DbSet<DailyStats> DailyStats { get; set; }
        public DbSet<RowData> RowData { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
