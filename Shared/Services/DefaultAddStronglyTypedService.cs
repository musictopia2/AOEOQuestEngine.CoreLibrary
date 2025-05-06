namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class DefaultAddStronglyTypedService(QuestDataContainer container) : BaseAddStronglyTypedService
{
    public override void ApplyTechsToTree()
    {
        //this should be the only thing i need.
        //this time, don't worry about extensions.   just do here.
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
            if (item.Units.Count == 0)
            {
                current.Effects = item.Effects;
                current.Prereqs = item.Prereqs;
            }
            else
            {
                //figure out the units in the new system.
                AddUnit(current, item, container.CivAbb, counter);
            }
            AdditionalTechs.Add(current);

        }
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