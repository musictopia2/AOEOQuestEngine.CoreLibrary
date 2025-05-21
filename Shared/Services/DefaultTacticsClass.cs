namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultTacticsClass(QuestDataContainer container) : ITacticsAutomation
{
    XElement ITacticsAutomation.GetAutomatedTactics(XElement source, bool fromDock)
    {
        UnitCounter counter = new();
        foreach (var tech in container.TechData.AllTechs)
        {
            foreach (var unit in tech.Units)
            {
                AddItem(source, unit, counter.GetNextUnitId);
            }
            if (tech.VillagersToSpawn > 0)
            {
                //do the villager part here.
                CustomUnitModel villager = new(CustomVillagerClass.SupportedProtoName, tech.VillagersToSpawn);
                AddItem(source, villager, counter.GetNextUnitId);
            }
        }
        foreach (var consumable in container.Consumables)
        {
            if (consumable.Units.Count > 0)
            {
                consumable.MaxUses.Times(x =>
                {
                    foreach (var unit in consumable.Units)
                    {
                        AddItem(source, unit, counter.GetNextUnitId);
                    }
                });
            }
            if (consumable.VillagersToSpawn > 0)
            {
                consumable.MaxUses.Times(x =>
                {
                    CustomUnitModel villager = new(CustomVillagerClass.SupportedProtoName, consumable.VillagersToSpawn);
                    AddItem(source, villager, counter.GetNextUnitId);
                });
            }

        }
        counter.Reset();
        string text;
        XElement tactic = source.Element("tactic")!;
        foreach (var tech in container.TechData.AllTechs)
        {
            foreach (var unit in tech.Units)
            {
                text = $"""
                <action>Spawn_{unit.ProtoName}{counter.GetNextUnitId}</action>
                """;
                tactic.Add(XElement.Parse(text));
            }
            if (tech.VillagersToSpawn > 0)
            {
                text = $"""
                <action>Spawn_{CustomVillagerClass.SupportedProtoName}{counter.GetNextUnitId}</action>
                """;
                tactic.Add(XElement.Parse(text));
            }
        }
        if (container.Consumables.Count == 0)
        {
            return source;
        }
        foreach (var consumable in container.Consumables)
        {

            if (consumable.Units.Count > 0)
            {
                consumable.MaxUses.Times(x =>
                {
                    foreach (var unit in consumable.Units)
                    {
                        text = $"""
                        <action>Spawn_{unit.ProtoName}{counter.GetNextUnitId}</action>
                        """;
                        tactic.Add(XElement.Parse(text));
                    }
                });
            }
            if (consumable.VillagersToSpawn > 0)
            {
                consumable.MaxUses.Times(x =>
                {
                    text = $"""
                    <action>Spawn_{CustomVillagerClass.SupportedProtoName}{counter.GetNextUnitId}</action>
                    """;
                    tactic.Add(XElement.Parse(text));
                });
            }
        }
        return source;
    }
    private static void AddItem(XElement source, CustomUnitModel unit, int id)
    {
        string text;
        text = $"""
            <action>
            	<name>Spawn_{unit.ProtoName}{id}</name>
            	<type>Maintain</type>
            	<MaintainEntry>
            	  <TrainCount>{unit.HowMany}</TrainCount>
            	  <UnitsOwned>{unit.HowMany}</UnitsOwned>
            	  <RateMultiplier>1.0</RateMultiplier>
            	  <TrainProtoUnit>{unit.ProtoName}</TrainProtoUnit>
            	</MaintainEntry>
            	<active>0</active>
            	<singleuse>1</singleuse>
            	<persistent>1</persistent>
            </action>
            """;
        source.Add(XElement.Parse(text));
    }
}