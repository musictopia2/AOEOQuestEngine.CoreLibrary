namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultUnitProcessor(QuestDataContainer container,
    IUnitRegistry consumableRegister,
    ITrainableUnitRegistry trainableRegister
    ) : IUnitProcessor
{
    public static string GetCosts(TrainableUnitModel unit)
    {
        if (unit.Costs.Count == 0)
        {
            throw new CustomBasicException("Cannot be free");
        }
        StrCat cats = new();
        foreach (var item in unit.Costs)
        {
            string text = $"""
                <Cost resourcetype="{item.Resource}">{item.Value}</Cost>
                """;
            cats.AddToString(text, Constants.VBCrLf);
        }
        return cats.GetInfo();
    }
    // Helper method to get available columns for units
    private static BasicList<int> GetAvailableUnitColumns(bool fromTownCenter)
    {
        // Logic for determining available columns for units (same as before)
        return fromTownCenter ? [6, 7, 8] : [7, 8];
    }
    private static BasicList<int> GetAvailableTechColumns(XElement unitSource)
    {
        const int maxColumns = 9; // Define the number of columns (as per your previous logic)
        // Create a HashSet of used columns for row 1
        var usedColumns = unitSource.Elements("Tech")
            .Where(x => (int?)x.Attribute("row") == 1) // Filter for row 1
            .Select(x => (int?)x.Attribute("column") ?? 0) // Get the column number, default to 0
            .ToHashSet();
        // Return a list of available columns
        BasicList<int> availableColumns = [];
        for (int column = 0; column < maxColumns; column++)
        {
            if (!usedColumns.Contains(column)) // If column is not used, add to available list
            {
                availableColumns.Add(column);
            }
        }
        availableColumns.RemoveFirstItem(); //i think i should remove the first item so it gets shifted just a little
        return availableColumns;
    }
    private void PopulateFortress(XElement entire)
    {
        //this is the unit part.
        //hopefully just those 2 parts needed (?)
        //Pe_Bldg_TownCenter
        //Pe_Bldg_Fortress //if i am testing with the persians.
        string name;
        name = $"{container.CivAbb}_Bldg_Fortress";
        XElement originalUnit = entire.Elements().Single(x => x.Attribute("name")!.Value == name);
        var available = GetAvailableUnitColumns(false).GetEnumerator();
        foreach (var item in container.TrainableUnits)
        {
            if (!available.MoveNext())
            {
                throw new InvalidOperationException("Too many techs; no available placement slots.");
            }
            //i can only support 2 at the most for this system.
            //one can be advisor and the other can be custom.
            int column = available.Current;
            XElement newTech = new("Train",
                new XAttribute("row", 0),
                new XAttribute("page", "0"),
                new XAttribute("column", column),
                item.Name);
            originalUnit.Add(newTech);
        }
    }

    private void PopulateTrainableUnits(XElement entire)
    {
        if (container.TrainableUnits.Count == 0)
        {
            return;
        }
        PopulateFortress(entire);
        foreach (var item in container.TrainableUnits)
        {
            var reals = trainableRegister.GetHandlerFor(item.Name);
            reals.Populate(entire, item, container);
        }
    }


    XElement IUnitProcessor.GetUnitXML()
    {
        XElement entire = XElement.Load(dd1.RawUnitLocation);

        PopulateTrainableUnits(entire);
        ProcessTownCenter(entire);


        foreach (var item in container.TechData.AllTechs)
        {
            HashSet<string> units = [];
            foreach (var temp in item.Units)
            {
                units.Add(temp.ProtoName);
            }
            foreach (var temp in units)
            {
                IUnitHandler? unit = consumableRegister.GetHandlerFor(temp);
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


    private void ProcessTownCenter(XElement entire)
    {
        string name;
        name = $"{container.CivAbb}_Bldg_TownCenter";
        XElement originalUnit = entire.Elements().Single(x => x.Attribute("name")!.Value == name);
        //this is the unit part.
        //hopefully just those 2 parts needed (?)
        //Pe_Bldg_TownCenter
        var availableTechs = GetAvailableTechColumns(originalUnit).GetEnumerator();
        var availableConsumables = GetAvailableUnitColumns(true).GetEnumerator();

        container.TechData.AllTechs.ForConditionalItems(x => x.IsOnDemand, (tech) =>
        {
            if (tech.VillagersToSpawn > 0)
            {
                if (!availableConsumables.MoveNext())
                {
                    throw new InvalidOperationException("Too many consumables; no available placement slots.");
                }
                int column = availableConsumables.Current;
                XElement newTech = new("Tech",
                    new XAttribute("row", 0),
                    new XAttribute("page", "0"),
                    new XAttribute("column", column),
                    tech.Name);
                originalUnit.Add(newTech);
            }
            else if (tech.Units.Count > 0)
            {
                foreach (var unit in tech.Units)
                {
                    if (!availableConsumables.MoveNext())
                    {
                        throw new InvalidOperationException("Too many consumables; no available placement slots.");
                    }
                    int column = availableConsumables.Current;
                    XElement newTech = new("Tech",
                        new XAttribute("row", 0),
                        new XAttribute("page", "0"),
                        new XAttribute("column", column),
                        tech.Name);
                    originalUnit.Add(newTech);
                }
            }
            else
            {
                if (!availableTechs.MoveNext())
                {
                    throw new InvalidOperationException("Too many techs; no available placement slots.");
                }
                int column = availableTechs.Current;
                XElement newTech = new("Tech",
                    new XAttribute("row", 1),
                    new XAttribute("page", "0"),
                    new XAttribute("column", column),
                    tech.Name);
                originalUnit.Add(newTech);
            }
        });
    }
}