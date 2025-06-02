namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class NoOpSpartaQuestEndedService(IQuestResultPersistenceService persist) : ISpartaQuestEnded
{
    async Task ISpartaQuestEnded.EndQuestAsync(EnumSpartaQuestResult result, string time)
    {
        await persist.ClearPendingAsync();
    }
}