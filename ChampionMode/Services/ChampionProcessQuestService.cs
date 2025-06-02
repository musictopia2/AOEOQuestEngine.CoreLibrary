namespace AOEOQuestEngine.CoreLibrary.ChampionMode.Services;
public class ChampionProcessQuestService(
    ChampionSharedQuestProcessor processor,
    QuestFileContainer questFileContainer,
    IQuestOutcomeRecoveryService recovery
    ) : IProcessQuestService
{
    async Task IProcessQuestService.ProcessQuestAsync()
    {
        bool rets;
        rets = await recovery.CanProceedWithQuestAsync();
        if (rets == false)
        {
            return; //something else will happen.
        }
        if (ll1.MainLocation == "")
        {
            throw new CustomBasicException("Must set up ahead of time now.  Since locations can change");
        }
        if (dd1.NewGamePath == "")
        {
            throw new CustomBasicException("Must set the new game path ahead of time now");
        }
        if (string.IsNullOrWhiteSpace(questFileContainer.FileName))
        {
            throw new CustomBasicException("Must set the quest file ahead of time");
        }
        await processor.ProcessQuestAsync(questFileContainer.QuestPath);
    }
}