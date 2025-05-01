namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class TechIDManager
{
    private int _currentID;
    public int GetNextID => _currentID;
    public void RegisterTech()
    {
        _currentID++; //this means will increment the id.
    }
    public void Clear()
    {
        //this needs to clear the new id manager.
        string path = dd1.RawTechLocation;
        XElement source = XElement.Load(path);
        var list = source.Elements().ToBasicList();
        _currentID = list.Count;
    }
}