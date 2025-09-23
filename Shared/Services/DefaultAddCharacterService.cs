namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultAddCharacterService(QuestDataContainer container) : BaseCharacterFileModifierService
{
    public override void ModifyCharacterFile()
    {
        if (HasVillagers() == false)
        {
            return;
        }
        this.AddCustomVillagers(container.CivAbb);
        AddMiscTechs();
        this.SaveCharacterFile();
    }
    protected virtual void AddMiscTechs()
    {

    }
    protected bool HasVillagers()
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