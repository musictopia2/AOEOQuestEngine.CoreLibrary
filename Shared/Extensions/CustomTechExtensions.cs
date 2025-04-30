namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class CustomTechExtensions
{
    public static void AddQuickCompleteQuest(this IQuestSettings settings)
    {
        BasicList<BasicEffectModel> list = [];
        BasicEffectModel effect;
        //this will help with not only when a quest is completed but also showing quests that a human gets forever.
        effect = new HitPointsModel() //seem to be okay
        {
            ProtoUnit = "all",
            Value = "100.0000"
        };
        list.Add(effect);
        effect = new DamageModel() //seem to be okay
        {
            ProtoUnit = "all",
            Value = "1000.0000"
        };
        list.Add(effect);
        settings.HumanForeverEffects.AddRange(list);
        settings.SeeAllMap = true;
    }
    public static void AddExperimentBuild(this IQuestSettings questSettings)
    {
        //this allows for testing so you can quickly test many conditions like population, age, tech researched, etc.
        BasicEffectModel effect;
        effect = new TechAllCostAllModel()
        {
            Value = "0.1000"
        };
        questSettings.HumanForeverEffects.Add(effect);
        effect = new TechAllResearchPointsModel()
        {
            Value = "0.1000"
        };
        questSettings.HumanForeverEffects.Add(effect);
        questSettings.HumanForeverEffects.AddRange(EffectsServices.GetStartingResources("5000"));
        effect = new BuildPointsModel()
        {
            ProtoUnit = "building",
            Value = "0.1000"
        };
        questSettings.HumanForeverEffects.Add(effect);
        effect = new CostAllModel()
        {
            ProtoUnit = "all",
            Value = "0.1000"
        };
        questSettings.HumanForeverEffects.Add(effect);

        effect = new TrainingPointsModel()
        {
            ProtoUnit = "unit",
            Value = "0.1000"
        };
        questSettings.HumanForeverEffects.Add(effect);
        effect = new PopulationCapAdditionModel()
        {
            ProtoUnit = uu1.TownCenter,
            Value = "200.00"
        };
        questSettings.HumanForeverEffects.Add(effect);
        effect = new PopulationExtraModel()
        {
            ProtoUnit = uu1.TownCenter,
            Value = "200.00"
        };
        questSettings.HumanForeverEffects.Add(effect);
    }
}