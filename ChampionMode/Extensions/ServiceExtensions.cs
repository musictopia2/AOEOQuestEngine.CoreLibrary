namespace AOEOQuestEngine.CoreLibrary.ChampionMode.Extensions;
public static class ServiceExtensions
{
    extension (IServiceCollection services)
    {
        public IServiceCollection RegisterChampionModeProcessingServices(Action<IServiceCollection> additionalActions)
        {
            services.AddSingleton<IProcessQuestService, ChampionProcessQuestService>()
                .AddSingleton<IQuestOutcomeRecoveryService, DefaultQuestOutcomeRecoveryService>()
                .AddSingleton<ChampionSharedQuestProcessor>()
                .AddSingleton<IQuestResultPersistenceService, JsonQuestResultPersistenceService>()
                .RegisterCoreOfflineServices()
                .RegisterCoreQuestQuestProcessorServices()
                .RegisterStandardQuestServices()
                .RegisterNoLaunchSpartanServices();
            additionalActions.Invoke(services); //major but here.
            return services;
        }
        public IServiceCollection RegisterChampionModeTestServices<L>(Action<IServiceCollection> additionalActions)
            where L : class, IQuestLocatorService
        {
            services.RegisterBasicsForTesting(services =>
            {
                services.AddSingleton<IQuestLocatorService, L>()
                        .AddSingleton<ChampionSharedQuestProcessor>();
                services.AddSingleton<IPlayQuestViewModel, ChampionTestSingleQuestViewModel>();
                services.RegisterCoreOfflineServices()
                .AddSingleton<IQuestResultPersistenceService, NoOpQuestResultPersistanceService>()
                .RegisterStandardQuestServices()
                .RegisterNoLaunchSpartanServices(); //if they do this, no launcher for sparta.
                additionalActions.Invoke(services);
            });
            return services;
        }
    }
    
}