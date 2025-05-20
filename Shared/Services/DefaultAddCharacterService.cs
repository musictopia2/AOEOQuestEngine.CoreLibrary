namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultAddCharacterService(QuestDataContainer container) : BaseTechsCharacterService
{
    public override void AddTechs()
    {
        if (HasVillagers() == false)
        {
            return;
        }
        this.AddCustomVillagers(container.CivAbb);
        this.SaveTechs();
    }
    private bool HasVillagers()
    {
        if (container.TechData.AllTechs.Any(x => x.VillagersToSpawn > 0))
        {
            return true;
        }
        if (container.Consumables.Any(x => x.VillagersToSpawn > 0))
        {
            return true;
        }
        return false;

    }
}