namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface IQuestOutcomeRecoveryService
{
    /// <summary>
    /// Handles any pending quest results (completed or failed).
    /// Returns false if a previous quest was successfully completed and fully processed.
    /// Returns true if it's safe to continue launching a new quest (e.g., after a failure or no previous result).
    /// </summary>
    Task<bool> HandlePreviousOutcomeIfNeededAsync();
}