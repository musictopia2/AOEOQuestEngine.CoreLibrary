namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultQuestExtensionApplier(QuestDataContainer container) : IQuestExtensionApplier
{
    void IQuestExtensionApplier.ApplyExtensions(XElement source)
    {
        container.ApplySimpleQuestSettings(source);
        BasicList<int> humanIds = [];
        BasicList<int> computerIds = [];
        BasicList<CustomTechModel> humanList = [];
        BasicList<CustomTechModel> computerList = [];
        foreach (var tech in container.TechData.AllTechs)
        {
            int id = container.GetNextTechID;
            if (tech.RecipientType == EnumRecipentType.Human || tech.RecipientType == EnumRecipentType.GlobalSharedRMS)
            {
                humanIds.Add(id);
                humanList.Add(tech);
            }
            if (tech.RecipientType == EnumRecipentType.Computer || tech.RecipientType == EnumRecipentType.GlobalSharedRMS)
            {
                computerIds.Add(id);
                computerList.Add(tech);
            }
        }
        //one more at the end here.

        string value;
        if (humanIds.Count > 0)
        {
            value = GetActivatedEffects(humanIds, humanList);
            source.AddMapStringVariable("CustomHumanMultipleFlexibleTechs", value);
        }
        if (computerIds.Count > 0)
        {
            value = GetActivatedEffects(computerIds, computerList);
            source.AddMapStringVariable("CustomComputerMultipleFlexibleTechs", value);
        }
        StrCat cats = new();
        foreach (var c in container.TrainableUnits)
        {
            cats.AddToString(c.Name, ",");
        }
        string finalText = cats.GetInfo();
        source.AddMapStringVariable("TrainUnits", finalText);


        BasicList<int> consumableIds = [];
        BasicList<int> others = [];
        foreach (var consumable in container.Consumables)
        {
            consumable.MaxUses.Times(x =>
            {
                int id = container.GetNextTechID;
                if (x == 1)
                {
                    consumableIds.Add(id);
                }
            });
        }
        container.Consumables.ForConditionalItems(x => x.HasExtraTechs(), _ =>
        {
            others.Add(container.GetNextTechID);
        });
        value = GetActivatedEffects(consumableIds, others, container.Consumables);
        source.AddMapStringVariable("Consumables", value);
    }

    private static string GetActivatedEffects(BasicList<int> ids, BasicList<CustomTechModel> techs)
    {
        if (ids.Count != techs.Count)
        {
            throw new ArgumentException("The ids and effects lists must have the same length.");
        }
        var parts = new List<string>();
        for (int i = 0; i < ids.Count; i++)
        {
            int techId = ids[i];
            var effect = techs[i];

            var innerParts = new List<string>
        {
            $"TechID:{techId}",
            $"ActivationType:{effect.ActivationType}"
        };

            switch (effect.ActivationType)
            {
                case EnumActivationType.Delayed:
                case EnumActivationType.LimitedTime:
                    innerParts.Add($"Time:{effect.Time}");
                    break;

                case EnumActivationType.Timespan:
                    innerParts.Add($"StartTime:{effect.StartTime}");
                    innerParts.Add($"EndTime:{effect.EndTime}");
                    break;
            }
            if (effect.Units.Count > 0 || effect.VillagersToSpawn > 0)
            {
                innerParts.Add("CustomUnit:True");
            }
            else
            {
                innerParts.Add("CustomUnit:False");
            }
            parts.Add(string.Join(",", innerParts));
        }
        return string.Join(";", parts);
    }

    private static string GetActivatedEffects(BasicList<int> ids, BasicList<int> others, BasicList<SimpleUnitConsumableModel> consumables)
    {
        if (ids.Count != consumables.Count)
        {
            throw new CustomBasicException("The id and consumable counts don't match");
        }
        int c = 0;
        var parts = new List<string>();
        for (int i = 0; i < ids.Count; i++)
        {
            int techId = ids[i];
            var consumable = consumables[i];
            string customUnit;
            if (consumable.Units.Count > 0 || consumable.VillagersToSpawn > 0)
            {
                customUnit = "True";
            }
            else
            {
                customUnit = "False";
            }
            int other;
            if (consumable.HasExtraTechs())
            {
                other = others[c];
                c++;
            }
            else
            {
                other = 0;
            }
            var innerParts = new List<string>
            {
                $"TechID:{techId}",
                $"DisplayName:{consumable.DisplayName}",
                $"Cooldown:{consumable.Cooldown}",
                $"MaximumUses:{consumable.MaxUses}",
                $"ActiveTime:{consumable.ActiveTime}",
                $"CustomUnit:{customUnit}",
                $"Other:{other}",
                $"Computer:{consumable.ForComputer}"
            };

            parts.Add(string.Join(",", innerParts));
        }
        return string.Join(";", parts);
    }
}