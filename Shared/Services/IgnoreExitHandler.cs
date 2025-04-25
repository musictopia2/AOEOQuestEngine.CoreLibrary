namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class IgnoreExitHandler : ISpartanExitHandler
{
    Task ISpartanExitHandler.ExitSpartanAsync(EnumSpartaExitStage stage)
    {
        return Task.CompletedTask;
    }
}