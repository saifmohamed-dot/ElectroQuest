
namespace ElectroQuest.Application.Analytics.Interfaces.Adapters
{
    public interface IConsumeMessage
    {
        Task ConsumeAsync(Func<string,Task<bool>> onRecieve);
    }
}
