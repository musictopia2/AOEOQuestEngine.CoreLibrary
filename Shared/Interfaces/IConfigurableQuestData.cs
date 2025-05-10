namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface IConfigurableQuestData
{
    TechMatrixService TechData { get; }
    BasicList<TrainableUnitModel> TrainableUnits { get; set; }
    string GetNextName(string key);
    //needs to do through here now.
    int DelayedAttackTime { get; set; }
    bool SeeAllMap { get; set; }
    EnumQuestNotificationMode QuestNotificationMode { get; set; }
    //if a person needs to know what civilization they are playing as, they have to bring in the proper context.

}