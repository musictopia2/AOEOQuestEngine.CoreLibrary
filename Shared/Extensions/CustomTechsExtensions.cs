namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class CustomTechsExtensions
{
    //so somebody can just have global forever with self heal if necessary.
    extension (SimpleUnitConsumableModel consumable)
    {
        internal bool HasExtraTechs
        {
            get
            {
                if (consumable.ForComputer)
                {
                    return true;
                }
                if (consumable.ActiveTime == 0)
                {
                    return false;
                }
                if (consumable.VillagersToSpawn > 0)
                {
                    return true; //try to force extra tech for villagers now. even though its only a filler.
                }
                if (consumable.Units.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
    extension (IConfigurableQuestData config)
    {
        public IConfigurableQuestData StartGlobalForeverActivationWithSelfHeal()
        {
            config.TechData.StartGlobalForeverActivation();
            config.ApplyGlobalSelfHealing();
            return config;
        }
        private void ApplyGlobalSelfHealing()
        {
            var effect = EffectsServices.GetEnableSelfHeal(uu1.Unit);
            config.TechData.AddEffect(effect);
        }
        public IConfigurableQuestData AddQuickCompleteQuest()
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
            config.TechData.StartHumanForeverActivation();
            config.TechData.AddSeveralEffects(list);
            config.SeeAllMap = true;
            return config;
        }
        public IConfigurableQuestData AddExperimentBuild()
        {
            //this allows for testing so you can quickly test many conditions like population, age, tech researched, etc.
            config.TechData.StartHumanForeverActivation();
            BasicEffectModel effect;
            effect = new TechAllCostAllModel()
            {
                Value = "0.1000"
            };
            config.TechData.AddEffect(effect);
            effect = new TechAllResearchPointsModel()
            {
                Value = "0.1000"
            };
            config.TechData.AddEffect(effect);
            config.TechData.AddSeveralEffects(EffectsServices.GetStartingResources("5000"));
            effect = new BuildPointsModel()
            {
                ProtoUnit = "building",
                Value = "0.1000"
            };
            config.TechData.AddEffect(effect);
            effect = new CostAllModel()
            {
                ProtoUnit = "all",
                Value = "0.1000"
            };
            config.TechData.AddEffect(effect);

            effect = new TrainingPointsModel()
            {
                ProtoUnit = "unit",
                Value = "0.1000"
            };
            config.TechData.AddEffect(effect);
            effect = new PopulationCapAdditionModel()
            {
                ProtoUnit = uu1.TownCenter,
                Value = "200.00"
            };
            config.TechData.AddEffect(effect);
            effect = new PopulationExtraModel()
            {
                Value = "200.00"
            };
            config.TechData.AddEffect(effect);
            return config;
        }
    }
    
}