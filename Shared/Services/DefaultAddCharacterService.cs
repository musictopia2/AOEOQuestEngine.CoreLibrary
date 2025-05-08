namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultAddCharacterService(QuestDataContainer container) : BaseTechsCharacterService
{
    public override void AddTechs()
    {
        if (container.TechData.AllTechs.Any(x => x.VillagersToSpawn > 0) == false)
        {
            return;
        }
        this.AddCustomVillagers(container.CivAbb);
        this.SaveTechs();
    }
}