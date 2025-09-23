namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public class RemoveOptionalChallengesTransformer : ISecondaryObjectiveTransformer
{
    public void Transform(XElement questXml)
    {
        questXml.RemoveOptionalChallenges();
    }
}