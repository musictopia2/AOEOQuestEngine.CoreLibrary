namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class OnlyChristmasRule(IDateOnlyPicker picker) : ChristmasSeasonRuleBase(picker)
{
    public override bool IsAllowed(out string message)
    {
        if (IsChristmasSeason == true)
        {
            message = "";
            return true;
        }
        message = ChristmasOnlyMessage;
        return false;
    }
}