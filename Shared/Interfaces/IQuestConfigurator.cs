namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface IQuestConfigurator
{
    Task ConfigureAsync(IQuestSettings questSettings);
}