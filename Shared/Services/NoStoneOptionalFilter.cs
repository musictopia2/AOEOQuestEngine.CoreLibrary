namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class NoStoneOptionalFilter : IEffectRemovalFilter
{
    public bool ShouldRemove(BasicEffectModel effect)
    {
        // Example check: GrantsStone could be a property you implement on BasicEffectModel
        return effect.GrantsStone;
    }
}