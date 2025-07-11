namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class SimpleUnitExtensions
{
    //must be public now since its now possible to use outside for experimenting.
    /// <summary>
    /// Adds a full custom unit XML element to the entire list of units.
    /// This appends the unit definition directly under the root unit collection element.
    /// </summary>
    public static void AddCustomUnitToEntireList(this XElement source, string details)
    {
        XElement extras = XElement.Parse(details);
        source.Add(extras);
    }
}