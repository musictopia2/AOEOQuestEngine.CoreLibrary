namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DoNothingSpartanReady : ISpartanReady
{
    Task ISpartanReady.LoadQuestAsync()
    {
        return Task.CompletedTask;
    }
}