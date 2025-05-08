namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class CharacterExtensions
{
    public static IAddTechsToCharacterService AddCustomVillagers(this IAddTechsToCharacterService service, string civAbb)
    {
        XElement source = service.Source!; // The character file being modified
        string originalName = $"{civAbb}_Civ_Villager";
        string customName = CustomVillagerClass.SupportedProtoName;
        var protounitsElement = source.Element("protounits");
        if (protounitsElement == null)
        {
            protounitsElement = new XElement("protounits");
            source.Add(protounitsElement);
        }
        if (protounitsElement.Elements("protounit").Any(pu => (string?)pu == customName))
        {
            return service; // Early exit to avoid duplicates
        }
        protounitsElement.Add(new XElement("protounit", customName));
        var traitsElement = source.Element("traits")
            ?? throw new InvalidOperationException("No <traits> element found in the character XML.");
        var originalTraits = traitsElement.Elements("trait")
            .Where(trait => (string?)trait.Attribute("protounit") == originalName)
            .ToBasicList();
        foreach (var trait in originalTraits)
        {
            var clonedTrait = new XElement(trait); // Deep clone
            clonedTrait.SetAttributeValue("protounit", customName); // Set to custom villager
            traitsElement.Add(clonedTrait);
        }
        return service;
    }
}