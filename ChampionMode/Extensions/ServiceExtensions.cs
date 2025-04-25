
namespace AOEOQuestEngine.CoreLibrary.ChampionMode.Extensions;
public static class ServiceExtensions
{
    // Registers basic services for Champion Mode tests.
    // The `additionalservices` action allows for further custom configurations.
    public static IServiceCollection RegisterAOEOTestsChampionMode<L>(this IServiceCollection services,
        Action<IServiceCollection> additionalservices
        )
        where L : class, IQuestLocatorService
    {
        services.RegisterBasicsForTesting(services =>
        {
            //if you need monitoring, you have to use windows stuff.
            services.AddSingleton<IQuestLocatorService, L>()
                .AddSingleton<IAddTechsToCharacterService, NoTechsCharacterService>()
                .AddSingleton<IAddTechsToTechTreeService, ChampionCustomTechClass>();
            services.RegisterCoreOfflineServices()
            .RegisterStandardQuestServices()
            .RegisterNoLaunchSpartanServices() //if they do this, no launcher for sparta.
            ;
            additionalservices.Invoke(services);
        });
        return services;
    }
    //the shared has to happen somewhere else.
    public static IServiceCollection RegisterChampionModeProcessingServices<Q> (this IServiceCollection services, Action<IServiceCollection> additionalActions)
        where Q : class, IProcessQuestService
    {
        services.AddSingleton<IProcessQuestService, Q>()
            .RegisterCoreOfflineServices()
            .RegisterCoreQuestQuestProcessorServices()
            .AddSingleton<IAddTechsToCharacterService, NoTechsCharacterService>()
            .AddSingleton<IAddTechsToTechTreeService, ChampionCustomTechClass>();
        services.RegisterCoreOfflineServices()
         .RegisterStandardQuestServices()
         .RegisterNoLaunchSpartanServices()
        ;
        additionalActions?.Invoke(services); //major but here.
        return services;
    }
}