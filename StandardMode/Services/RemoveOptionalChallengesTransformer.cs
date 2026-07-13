namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public class RemoveOptionalChallengesTransformer : ISecondaryObjectiveTransformer
{
    public void Transform(XElement questXml)
    {
        questXml.RemoveTimers(); //chose to remove timers.
        questXml.RemoveOptionalChallenges();
    }
}