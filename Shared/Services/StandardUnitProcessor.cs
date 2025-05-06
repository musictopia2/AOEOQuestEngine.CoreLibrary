namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class StandardUnitProcessor(QuestDataContainer container, IUnitRegistry register) : IUnitProcessor
{
    XElement IUnitProcessor.GetUnitXML()
    {
        XElement entire = XElement.Load(dd1.RawUnitLocation);
        foreach (var item in container.TechData.AllTechs)
        {

            foreach (var temp in item.Units)
            {
                IUnitHandler? unit = register.GetHandlerFor(temp.ProtoName);
                unit?.ProcessCustomUnit(entire); //i broke it now.
            }
        }
        return entire;
    }
}