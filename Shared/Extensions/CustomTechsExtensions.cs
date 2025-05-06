namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class CustomTechsExtensions
{
    //so somebody can just have global forever with self heal if necessary.
    public static void StartGlobalForeverActivationWithSelfHeal(this IConfigurableQuestData config)
    {
        config.TechData.StartGlobalForeverActivation();
        config.ApplyGlobalSelfHealing();
    }
    public static void ApplyGlobalSelfHealing(this IConfigurableQuestData config)
    {
        var effect = EffectsServices.GetEnableSelfHeal(uu1.Unit);
        config.TechData.AddEffect(effect);
    }
    public static void AddQuickCompleteQuest(this IConfigurableQuestData configure)
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
        configure.TechData.StartHumanForeverActivation();
        configure.TechData.AddSeveralEffects(list);
        configure.SeeAllMap = true;
    }
    public static void AddExperimentBuild(this IConfigurableQuestData configure)
    {
        //this allows for testing so you can quickly test many conditions like population, age, tech researched, etc.
        configure.TechData.StartHumanForeverActivation();
        BasicEffectModel effect;
        effect = new TechAllCostAllModel()
        {
            Value = "0.1000"
        };
        configure.TechData.AddEffect(effect);
        effect = new TechAllResearchPointsModel()
        {
            Value = "0.1000"
        };
        configure.TechData.AddEffect(effect);
        configure.TechData.AddSeveralEffects(EffectsServices.GetStartingResources("5000"));
        effect = new BuildPointsModel()
        {
            ProtoUnit = "building",
            Value = "0.1000"
        };
        configure.TechData.AddEffect(effect);
        effect = new CostAllModel()
        {
            ProtoUnit = "all",
            Value = "0.1000"
        };
        configure.TechData.AddEffect(effect);

        effect = new TrainingPointsModel()
        {
            ProtoUnit = "unit",
            Value = "0.1000"
        };
        configure.TechData.AddEffect(effect);
        effect = new PopulationCapAdditionModel()
        {
            ProtoUnit = uu1.TownCenter,
            Value = "200.00"
        };
        configure.TechData.AddEffect(effect);
        effect = new PopulationExtraModel()
        {
            ProtoUnit = uu1.TownCenter,
            Value = "200.00"
        };
        configure.TechData.AddEffect(effect);
    }
}