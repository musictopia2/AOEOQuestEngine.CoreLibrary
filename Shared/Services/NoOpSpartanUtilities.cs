namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class NoOpSpartanUtilities : ISpartanUtilities
{
    void ISpartanUtilities.ExitSpartan()
    {

    }
    bool ISpartanUtilities.IsSpartanRunning()
    {
        return false;
    }
}