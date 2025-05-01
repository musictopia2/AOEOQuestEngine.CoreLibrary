namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultQuestPreparationHandler(IQuestSettings questSettings,
    IQuestConfigurator configurator,
    TechIDManager manager
    ) : IQuestPreparationHandler
{
    Task IQuestPreparationHandler.PrepareAsync()
    {
        manager.Clear();
        ResetQuestSettingsClass.ResetQuests(questSettings);
        return configurator.ConfigureAsync(questSettings);
    }
}