namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultQuestPreparationHandler(QuestDataContainer container, IQuestConfigurator config) : IQuestPreparationHandler
{
    async Task IQuestPreparationHandler.PrepareAsync()
    {
        await container.ClearAsync();
        await config.ConfigureAsync(container);
        container.TechData.AllTechs.ForEach(tech =>
        {
            tech.NormalizeTechState();
            tech.Validate();
        });
        container.TrainableUnits.ForEach(unit =>
        {
            unit.Validate();
        });
        container.ProcessConsumables();
    }
}