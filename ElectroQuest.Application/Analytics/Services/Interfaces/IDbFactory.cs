using Microsoft.EntityFrameworkCore;

namespace ElectroQuest.Application.Analytics.Services.Interfaces
{
    public interface IDbFactory
    {
        DbContext CreateDbContext();
    }
}
