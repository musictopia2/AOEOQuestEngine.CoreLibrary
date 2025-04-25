namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface ISpartanExitHandler
{
    Task ExitSpartanAsync(EnumSpartaExitStage stage);
}