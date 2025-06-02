namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface ISpartaQuestEnded
{
    /// <summary>
    /// Called when the quest ends, either from live monitoring or recovery.
    /// Handles success or failure outcomes and any required processing.
    /// </summary>
    /// <param name="result">Victory or failure result.</param>
    /// <param name="time">Duration of the quest (HH:mm:ss).</param>
    Task EndQuestAsync(EnumSpartaQuestResult result, string time);
}