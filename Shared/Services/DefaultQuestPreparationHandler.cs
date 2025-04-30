namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultQuestPreparationHandler(IQuestSettings questSettings,
    IQuestConfigurator configurator
    ) : IQuestPreparationHandler
{
    Task IQuestPreparationHandler.PrepareAsync()
    {
        ResetQuestSettingsClass.ResetQuests(questSettings);
        return configurator.ConfigureAsync(questSettings);
    }
}