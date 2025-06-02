namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class NoOpSpartaQuestEndedService : ISpartaQuestEnded
{
    Task ISpartaQuestEnded.EndQuestAsync(EnumSpartaQuestResult result, string time)
    {
        return Task.CompletedTask;
    }
}