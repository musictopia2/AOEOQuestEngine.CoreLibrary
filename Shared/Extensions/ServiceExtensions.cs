namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection RegisterStandardQuestServices(this IServiceCollection services)
    {
        services.AddSingleton<IQuestSettings, SimpleQuestSettings>()
            .AddSingleton<IQuestPreparationHandler, DefaultQuestPreparationHandler>() 
            .AddSingleton<IQuestExtensionApplier, AccommodationQuestExtensionApplier>()
            .AddSingleton<IQuestConfigurator, NoOpQuestConfigurator>()
            .AddSingleton<IGlobalTechStrategy, NoGlobalTechStrategy>()
            .AddSingleton<IUnitRegistry, NoUnitService>();
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
        services.AddSingleton<ITechBusinessService, TechBusinessService>()
            .AddSingleton<TechIDManager>()
            .AddSingleton<IAddTechsToCharacterService, NoTechsCharacterService>();
        services.AddSingleton<ITacticsBusinessService, BasicTacticsBusinessService>();
        services.AddSingleton<ITacticsAutomation, CustomTacticsClass>();
        services.AddSingleton<IUnitProcessor, StandardUnitProcessor>();
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