namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class NoOpQuestConfigurator : IQuestConfigurator
{
    Task IQuestConfigurator.ConfigureAsync(IConfigurableQuestData config)
    {
        return Task.CompletedTask;
    }
}