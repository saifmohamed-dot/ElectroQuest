namespace ElectroQuest.Application.Analytics.Interfaces.Adapters
{
    public interface IPublishMessage
    {
        Task PublishAsync<T>(T message , DateOnly date /* for loggin purpose */);
        Task CompletePublishNotify();
    }
}
