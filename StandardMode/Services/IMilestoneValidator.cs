namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public interface IMilestoneValidator
{
    void Validate(IReadOnlyList<string> techs);
}