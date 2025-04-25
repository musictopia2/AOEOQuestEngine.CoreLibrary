namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection RegisterStandardQuestServices(this IServiceCollection services)
    {
        services.AddSingleton<IQuestSettings, SimpleQuestSettings>()
            .AddSingleton<IQuestConfigurator, NoOpQuestConfigurator>()
            .AddSingleton<IGlobalTechStrategy, NoGlobalTechStrategy>()
            .AddSingleton<IUnitRegistry, NoUnitService>();
        return services;
    }
    public static IServiceCollection RegisterNoLaunchSpartanServices(this IServiceCollection services)
    {
        //this requires nothing else.
        services.AddSingleton<ISpartanLaunchHandler, DoNothingSpartanLaunchHandler>();
        return services;
    }
    public static IServiceCollection RegisterCoreOfflineServices(this IServiceCollection services)
    {
        //anything else that could be needed but are advanced services.
        services.AddSingleton<ITechBusinessService, TechBusinessService>();
        services.AddSingleton<ITacticsBusinessService, BasicTacticsBusinessService>();
        services.AddSingleton<ITacticsAutomation, CustomTacticsClass>();
        services.AddSingleton<IUnitProcessor, StandardUnitProcessor>();
        return services;
    }
    internal static IServiceCollection RegisterCoreQuestQuestProcessorServices(this IServiceCollection services)
    {
        services.RegisterBasicsForTesting(services =>
        {
            services.AddSingleton<QuestFileContainer>();
        });
        return services;
    }
}