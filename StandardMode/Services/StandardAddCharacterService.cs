namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public class StandardAddCharacterService(QuestDataContainer container,
    IMilestoneService milestoneService
    ) : DefaultAddCharacterService(container)
{
    protected override void AddMiscTechs()
    {
        var list = milestoneService.GetMilestoneTechs();
        //has to figure out how to add milestone techs
    }
}