namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultQuestPreparationHandler(QuestDataContainer container, IQuestConfigurator config) : IQuestPreparationHandler
{
    Task IQuestPreparationHandler.PrepareAsync()
    {
        container.Clear();
        return config.ConfigureAsync(container);
    }
}