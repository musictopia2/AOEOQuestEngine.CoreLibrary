namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class ConsoleSpartanPlayQuestService : IPlayQuestService
{
    void IPlayQuestService.OpenOfflineGame(string gamePath)
    {
        Console.WriteLine($"Mock playing quest with game path of {gamePath}   Please check results");
    }
}