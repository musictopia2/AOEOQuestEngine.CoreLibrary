namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface IQuestOutcomeRecoveryService
{
    /// <summary>
    /// Checks for and processes any pending quest outcome.
    /// Returns true if it's safe to proceed with a new quest (no pending victory).
    /// Returns false if a prior quest was successfully completed and has now been processed.
    /// </summary>
    Task<bool> CanProceedWithQuestAsync();
}