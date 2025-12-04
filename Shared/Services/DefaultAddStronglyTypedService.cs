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
            if (item.DisplayName != "")
            {
                current.DisplayNameID = container.InsertLocalizedString(item.DisplayName);
            }
            if (item.Details != "")
            {
                current.RolloverTextID = container.InsertLocalizedString(item.Details);
            }
            if (item.IsOnDemand)
            {
                current.ResearchPoints = item.ResearchPoints;
                current.Icon = @"Celeste\UserInterface\Icons\Techs\C08TechMahoutMastery_ua";
                current.Costs = item.Costs;
            }
            if (item.Units.Count == 0 && item.VillagersToSpawn == 0)
            {
                current.Effects = item.Effects;
                current.Prereqs = item.Prereqs;
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
        foreach (var consumable in container.Consumables)
        {
            foreach (var item in consumable.GeneratedTechs)
            {
                current = TechTreeServices.CreateNewTechModel(item.Name);
                current.DisplayNameID = container.InsertLocalizedString(item.DisplayName);
                current.RolloverTextID = container.InsertLocalizedString(item.Details);
                current.ResearchPoints = item.ResearchPoints;
                current.Icon = @"Celeste\UserInterface\Icons\Techs\C08TechMahoutMastery_ua";
                if (item.Units.Count > 0)
                {
                    AddUnit(current, item, container.CivAbb, counter);
                }
                else if (item.VillagersToSpawn > 0)
                {
                    AddVillager(current, item, container.CivAbb, counter);
                }
                else
                {
                    current.Effects = item.Effects;
                    current.Prereqs = item.Prereqs;
                }
                AdditionalTechs.Add(current);
            }
        }
        container.Consumables.ForConditionalItems(x => x.HasExtraTechs, item =>
        {
            BasicEffectModel effect;
            current = TechTreeServices.CreateNewTechModel();
            if (item.ForComputer)
            {
                current.Effects = item.Effects;
            }
            else if (item.Units.Count > 0)
            {
                //this is for unit effects.
                //because this is for consumable, then this will disappear but can come back.
                foreach (var unit in item.Units)
                {
                    //effect = EffectsServices.GetHitPoints(unit.ProtoName, "0.000000000000010000");
                    effect = EffectsServices.GetTerminateUnit(unit.ProtoName);
                    current.Effects.Add(effect);
                }
            }
            else if (item.VillagersToSpawn > 0)
            {
                effect = EffectsServices.GetTerminateUnit(CustomVillagerClass.SupportedProtoName);
                current.Effects.Add(effect);
            }
            else
            {
                throw new CustomBasicException("No extra tech needed");
            }
            AdditionalTechs.Add(current);
        });
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