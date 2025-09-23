namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public class NoMilestoneService : IMilestoneService
{
    IReadOnlyList<string> IMilestoneService.GetMilestoneTechs()
    {
        return [];
    }
}