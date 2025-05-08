namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class CustomVillagerClass(QuestDataContainer container) : IUnitHandler
{
    public static string SupportedProtoName => "CustomVillager";
    void IUnitHandler.ProcessCustomUnit(XElement root)
    {
        //don't worry about duplicates for now.
        string name;
        name = $"{container.CivAbb}_Civ_Villager";
        XElement originalUnit = root.Elements().Single(x => x.Attribute("name")!.Value == name);
        XElement copiedUnit = new (originalUnit);
        copiedUnit.Attribute("name")!.Value = SupportedProtoName;
        copiedUnit.Element("PopulationCount")!.Value = "1"; // Set to 1 for instant population
        copiedUnit.Element("TrainPoints")!.Value = "0.0000"; // Set to 0 for instant training
        //everything else is the same.
        root.Add(copiedUnit);
    }
}