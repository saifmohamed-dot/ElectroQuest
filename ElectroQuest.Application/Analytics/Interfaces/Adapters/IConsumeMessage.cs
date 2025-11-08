
namespace ElectroQuest.Application.Analytics.Interfaces.Adapters
{
    public interface IConsumeMessage
    {
        Task ConsumeAsync(Func<string,bool> onRecieve);
    }
}
