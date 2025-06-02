namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultQuestOutcomeRecoveryService(ISpartaQuestEnded ender, IQuestResultPersistenceService persist) : IQuestOutcomeRecoveryService
{
    async Task<bool> IQuestOutcomeRecoveryService.CanProceedWithQuestAsync()
    {
        var result = await persist.LoadPendingAsync();
        if (result is null)
        {
            return true;
        }
        if (result.Result == EnumSpartaQuestResult.Failed)
        {
            await ender.EndQuestAsync(result.Result, result.Time);
            return true;
        }
        if (result.Result == EnumSpartaQuestResult.Ongoing)
        {
            throw new CustomBasicException("Cannot be ongoing");
        }
        await ender.EndQuestAsync(result.Result, result.Time);
        return false;
    }
}