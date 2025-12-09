namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DisallowChristmasSeasonRule(IDateOnlyPicker picker) : ChristmasSeasonRuleBase(picker)
{
    public override bool IsAllowed(out string message)
    {
        if (IsChristmasSeason == false)
        {
            message = "";
            return true;
        }
        message = AnyButChristmasMessage;
        return false;
    }
}