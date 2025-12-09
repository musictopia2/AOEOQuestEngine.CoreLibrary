namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface ISeasonRuleEvaluator
{
    bool IsAllowed(out string message);
}