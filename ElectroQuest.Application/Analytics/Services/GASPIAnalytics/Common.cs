

namespace ElectroQuest.Application.Analytics.Services.GASPIAnalytics
{
    public static class Common
    {
        public static int MessageId = 0;
        public static SemaphoreSlim Start = new SemaphoreSlim(0 , 2);
    }
}
