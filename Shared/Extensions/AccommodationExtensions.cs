namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class AccommodationExtensions
{
    public static void AddAccommodationQuestExtensions(this XElement source,
        IQuestSettings settings, 
        IGlobalTechStrategy global,
        TechIDManager manager
        )
    {
        //the source is the quest file.
        if (global.HasGlobalTech)
        {
            source.AddGlobalTech(manager); //will have global stuff always for this version.
        }
        if (settings.DelayedAttackTime > 0)
        {
            source.SetDelayedAttacksForAllComputerPlayers(settings.DelayedAttackTime);
        }
        if (settings.HumanForeverEffects.Count > 0)
        {
            source.AddHumanTechForever(manager);
        }
        if (settings.HumanLimitedEffects.Count > 0)
        {
            source.AddHumanTechLimited(settings.LimitedHumanEffectTime, manager);
        }
        if (settings.ComputerForeverEffects.Count > 0)
        {
            source.AddComputerTechForever(manager);
        }
        if (settings.ComputerLimitedEffects.Count > 0)
        {
            source.AddComputerTechLimited(settings.LimitedComputerEffectTime, manager);
        }
        if (settings.Units.Count > 0)
        {
            source.AddAutomationTownCenterTech(manager);
        }
        if (settings.SeeAllMap)
        {
            source.AddMapBoolVariable("SeeAll", true);
        }
    }
}