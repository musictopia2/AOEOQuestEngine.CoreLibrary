namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class StandardUnitProcessor(IQuestSettings settings, IUnitRegistry register) : IUnitProcessor
{
    XElement IUnitProcessor.GetUnitXML()
    {
        XElement entire = XElement.Load(dd1.RawUnitLocation);
        HashSet<string> units = [];
        foreach (var item in settings.Units)
        {
            units.Add(item.ProtoName);
        }
        foreach (var item in units)
        {
            IUnitHandler? unit = register.GetHandlerFor(item);
            unit?.ProcessCustomUnit(entire);
        }
        return entire;
    }
}