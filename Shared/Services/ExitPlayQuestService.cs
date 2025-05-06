namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class ExitPlayQuestService(IExit exit) : IPlayQuestService
{
    void IPlayQuestService.OpenOfflineGame(string gamePath)
    {
        exit.ExitApp();
    }
}