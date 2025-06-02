namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class NoOpQuestOutcomeRecoveryService : IQuestOutcomeRecoveryService
{
    Task<bool> IQuestOutcomeRecoveryService.CanProceedWithQuestAsync()
    {
        return Task.FromResult(true);
    }
}