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
}