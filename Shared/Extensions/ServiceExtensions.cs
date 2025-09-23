namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection RegisterStandardQuestServices(this IServiceCollection services)
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
    public static IServiceCollection RegisterNoLaunchSpartanServices(this IServiceCollection services)
    {
        //this requires nothing else.
        services.AddSingleton<ISpartanLaunchHandler, DoNothingSpartanLaunchHandler>()
            .AddSingleton<IPostLaunchAction, NoOpPostLaunchAction>()
            .AddSingleton<ISpartanUtilities, NoOpSpartanUtilities>()
            .AddSingleton<QuestMonitoringEndingContainer>(); //better to be safe than sorry
        return services;
    }
    public static IServiceCollection RegisterCoreOfflineServices(this IServiceCollection services)
    {
        //anything else that could be needed but are advanced services.
        services.AddSingleton<ITechBusinessService, AdvancedTechBusinessService>()
            .AddSingleton<IRmsHandler, NoCopyRmsHandler>()
            .AddSingleton<QuestTitleContainer>()
            //.AddSingleton<IQuestResultPersistenceService, NoOpQuestResultPersistanceService>()
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
    public static IServiceCollection RegisterCoreQuestQuestProcessorServices(this IServiceCollection services)
    {
        services.RegisterBasicsForTesting(services =>
        {
            services.AddSingleton<QuestFileContainer>();
        });
        return services;
    }
}