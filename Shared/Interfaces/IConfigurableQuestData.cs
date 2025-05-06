namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface IConfigurableQuestData
{
    TechMatrixService TechData { get; }
    //needs to do through here now.
    int DelayedAttackTime { get; set; }
    bool SeeAllMap { get; set; }
    //if a person needs to know what civilization they are playing as, they have to bring in the proper context.

}