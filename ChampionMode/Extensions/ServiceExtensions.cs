namespace AOEOQuestEngine.CoreLibrary.ChampionMode.Extensions;
public static class ServiceExtensions
{
    //for test services, make it clear should use the windows services for testing single quests.


    //the shared has to happen somewhere else.
    public static IServiceCollection RegisterChampionModeProcessingServices (this IServiceCollection services, Action<IServiceCollection> additionalActions)
    {
        services.AddSingleton<IProcessQuestService, ChampionProcessQuestService>()
            .AddSingleton<ChampionSharedQuestProcessor>()
            .RegisterCoreOfflineServices()
            .RegisterCoreQuestQuestProcessorServices()
            .AddSingleton<IAddTechsToTechTreeService, ChampionCustomTechClass>()
            .RegisterStandardQuestServices()
            .RegisterNoLaunchSpartanServices()
        ;
        additionalActions?.Invoke(services); //major but here.
        return services;
    }
}