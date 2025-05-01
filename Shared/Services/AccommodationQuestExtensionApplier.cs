namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class AccommodationQuestExtensionApplier(IQuestSettings settings,
    IGlobalTechStrategy global,
    TechIDManager manager
    ) : IQuestExtensionApplier
{
    void IQuestExtensionApplier.ApplyExtensions(XElement source)
    {
        source.AddAccommodationQuestExtensions(settings, global, manager);
    }
}