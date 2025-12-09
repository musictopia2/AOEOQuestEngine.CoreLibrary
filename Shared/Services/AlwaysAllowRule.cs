namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class AlwaysAllowRule : ISeasonRuleEvaluator
{
    bool ISeasonRuleEvaluator.IsAllowed(out string message)
    {
        message = "";
        return true;
    }
}