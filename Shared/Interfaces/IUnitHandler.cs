namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface IUnitHandler
{
    abstract static string SupportedProtoName { get; }
    bool FromDock => false;
    //obviously is going to be the supported unit now.
    void ProcessCustomUnit(XElement root);
}