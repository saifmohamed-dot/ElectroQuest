namespace ElectroQuest.Application.Analytics.Interfaces.Adapters
{
    public interface IReadLocal
    {
        Task<TResult?> ReadLocalAsync<TResult>(string path , string type);
    }
}
