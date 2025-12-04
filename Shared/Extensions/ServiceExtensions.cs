namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class ServiceExtensions
{
    extension (IServiceCollection services)
    {
        public IServiceCollection RegisterStandardQuestServices()
        {
            services.AddSingleton<QuestDataContainer>()
                .AddSingleton<IQuestPreparationHandler, DefaultQuestPreparationHandler>()
                .AddSingleton<IQuestExtensionApplier, DefaultQuestExtensionApplier>()
                .AddSingleton<IQuestConfigurator, NoOpQuestConfigurator>()
                .AddSingleton<IUnitRegistry, NoUnitService>()
                .AddSingleton<ITrainableUnitRegistry, NoTrainableUnitsService>()
                ;
            return services;
        }
        public IServiceCollection RegisterNoLaunchSpartanServices()
        {
            //this requires nothing else.
            services.AddSingleton<ISpartanLaunchHandler, DoNothingSpartanLaunchHandler>()
                .AddSingleton<IPostLaunchAction, NoOpPostLaunchAction>()
                .AddSingleton<ISpartanUtilities, NoOpSpartanUtilities>()
                .AddSingleton<QuestMonitoringEndingContainer>(); //better to be safe than sorry
            return services;
        }
        public IServiceCollection RegisterCoreOfflineServices()
        {
            //anything else that could be needed but are advanced services.
            services.AddSingleton<ITechBusinessService, AdvancedTechBusinessService>()
                .AddSingleton<IRmsHandler, NoCopyRmsHandler>()
                .AddSingleton<QuestTitleContainer>()
                .AddSingleton<ISpartaQuestEnded, NoOpSpartaQuestEndedService>() //these 2 are now required  best to just now include here.  can override later anyways.
                .AddSingleton<IAddStronglyTypedTechsService, DefaultAddStronglyTypedService>()
                .AddSingleton<ICharacterFileModifierService, DefaultAddCharacterService>();
            services.AddSingleton<ITacticsBusinessService, BasicTacticsBusinessService>();
            services.AddSingleton<ITacticsAutomation, DefaultTacticsClass>();
            services.AddSingleton<IUnitProcessor, DefaultUnitProcessor>();
            services.AddSingleton<QuestRunContainer>(); //this is now core in order to make this work.
            return services;
        }
        //since a windows library has to use this as well.
        public IServiceCollection RegisterCoreQuestQuestProcessorServices()
        {
            services.RegisterBasicsForTesting(services =>
            {
                services.AddSingleton<QuestFileContainer>();
            });
            return services;
        }
    }
    
}