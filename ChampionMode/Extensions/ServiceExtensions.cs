namespace AOEOQuestEngine.CoreLibrary.ChampionMode.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection RegisterChampionModeProcessingServices(this IServiceCollection services, Action<IServiceCollection> additionalActions)
    {
        services.AddSingleton<IProcessQuestService, ChampionProcessQuestService>()
            .AddSingleton<ChampionSharedQuestProcessor>()
            .RegisterCoreOfflineServices()
            .RegisterCoreQuestQuestProcessorServices()
            .AddSingleton<IAddTechsToTechTreeService, ChampionCustomTechClass>()
            .RegisterStandardQuestServices()
            .RegisterNoLaunchSpartanServices();
        additionalActions.Invoke(services); //major but here.
        return services;
    }
    public static IServiceCollection RegisterChampionModeTestServices<L>(this IServiceCollection services, Action<IServiceCollection> additionalActions)
        where L : class, IQuestLocatorService
    {
        services.RegisterBasicsForTesting(services =>
        {
            services.AddSingleton<IQuestLocatorService, L>()
                    .AddSingleton<ChampionSharedQuestProcessor>()
                .AddSingleton<IAddTechsToCharacterService, NoTechsCharacterService>()
                .AddSingleton<IAddTechsToTechTreeService, ChampionCustomTechClass>();
            services.AddSingleton<IPlayQuestViewModel, ChampionTestSingleQuestViewModel>();
            services.RegisterCoreOfflineServices()
            .RegisterStandardQuestServices()
            .RegisterNoLaunchSpartanServices(); //if they do this, no launcher for sparta.

        });
        return services;
    }
}