namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class NoCopyRmsHandler : IRmsHandler
{
    Task IRmsHandler.CopyRmsFilesAsync()
    {
        return Task.CompletedTask;
    }
}