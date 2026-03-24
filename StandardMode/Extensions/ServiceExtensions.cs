namespace AOEOQuestEngine.CoreLibrary.StandardMode.Extensions;
public static class ServiceExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection RegisterStandardModeTestServices<L>(Action<IServiceCollection> additionalActions)
            where L : class, IQuestLocatorService
        {
            services.RegisterBasicsForTesting(services =>
            {
                services.AddSingleton<IQuestLocatorService, L>()
                        .AddSingleton<StandardSharedQuestProcessor>();
                services.AddSingleton<IPlayQuestViewModel, StandardTestSingleQuestViewModel>();
                services.RegisterCoreOfflineServices()
                .AddSingleton<IQuestResultPersistenceService, NoOpQuestResultPersistanceService>()
                .RegisterStandardQuestServices()
                .RegisterNoLaunchSpartanServices(); //if they do this, no launcher for sparta.
                additionalActions.Invoke(services);
            });
            return services;
        }
        public IServiceCollection RegisterRemoveEliteFromStandard()
        {
            services.AddKeyedSingleton<IQuestExtensionApplier, DefaultQuestExtensionApplier>("Standard")
                .AddSingleton<IQuestExtensionApplier, RemoveEliteQuestClass>();
            return services;
        }
    }
}