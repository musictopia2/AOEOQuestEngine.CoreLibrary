namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class StandardUnitProcessor(QuestDataContainer container, IUnitRegistry register) : IUnitProcessor
{
    XElement IUnitProcessor.GetUnitXML()
    {
        XElement entire = XElement.Load(dd1.RawUnitLocation);
        foreach (var item in container.TechData.AllTechs)
        {
            HashSet<string> units = [];
            foreach (var temp in item.Units)
            {
                units.Add(temp.ProtoName);
            }
            foreach (var temp in units)
            {
                IUnitHandler? unit = register.GetHandlerFor(temp);
                unit?.ProcessCustomUnit(entire); //i broke it now.
            }
        }
        if (container.TechData.AllTechs.Any(x => x.VillagersToSpawn > 0) == false)
        {
            return entire;
        }
        //now villager.
        IUnitHandler villager = new CustomVillagerClass(container);
        villager.ProcessCustomUnit(entire);
        return entire;
    }
}