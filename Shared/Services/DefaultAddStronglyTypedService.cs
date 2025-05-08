namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultAddStronglyTypedService(QuestDataContainer container) : BaseAddStronglyTypedService
{
    public override void ApplyTechsToTree()
    {
        BasicTechModel current;
        UnitCounter counter = new();
        foreach (var item in container.TechData.AllTechs)
        {
            bool global = false;
            if (item.RecipientType == EnumRecipentType.GlobalObtainable)
            {
                global = true;
            }
            if (string.IsNullOrWhiteSpace(item.Name) == false)
            {
                current = TechTreeServices.CreateNewTechModel(item.Name, global);
            }
            else
            {
                current = TechTreeServices.CreateNewTechModel(global);
            }
            if (item.Units.Count == 0 && item.VillagersToSpawn == 0)
            {
                current.Effects = item.Effects;
                current.Prereqs = item.Prereqs;
                //this is helpful for eventually having on demand techs.
                //if research points are set, then even with no cost, you don't get the tech.
                //unless you can figure out how to show a building to show you are going to get it.
                //current.ResearchPoints = "3.000"; //well see if i get in 3 seconds or not.
            }

            else if (item.Units.Count > 0)
            {
                //figure out the units in the new system.
                AddUnit(current, item, container.CivAbb, counter);
            }
            else
            {
                AddVillager(current, item, container.CivAbb, counter);
            }
            AdditionalTechs.Add(current);

        }
    }
    private static void AddVillager(BasicTechModel current,
        CustomTechModel activate,
        string civAbb,
        UnitCounter counter)
    {
        string name = CustomVillagerClass.SupportedProtoName;
        int index = counter.GetNextUnitId;
        string actionName = $"Spawn_{name}{index}";
        BasicEffectModel effect = EffectsServices.GetCustomTactic(actionName, $"{civAbb}_bldg_TownCenter");
        current.Effects.Add(effect);
        effect = EffectsServices.GetPopulationCapAdditional(uu1.TownCenter, "100.0000");
        effect.Relativity = "Absolute";
        current.Effects.Add(effect);
        current.Prereqs = activate.Prereqs;
    }
    private static void AddUnit(BasicTechModel current,
        CustomTechModel activate,
        string civAbb,
        UnitCounter counter
        )
    {
        BasicEffectModel effect;
        BasicList<BasicEffectModel> list = [];
        foreach (var unit in activate.Units)
        {
            if (unit.ProtoName == "")
            {
                throw new CustomBasicException("Proto name is blank.  Cannot add unit techs");
            }
            int index = counter.GetNextUnitId;
            string actionName = $"Spawn_{unit.ProtoName}{index}";
            effect = new CustomTacticEffect()
            {
                CustomTacticName = actionName,
                ProtoUnit = $"{civAbb}_bldg_TownCenter"
            };
            list.Add(effect);
        }
        current.Effects = list;
        current.Prereqs = activate.Prereqs; //something else adds it.
    }
}