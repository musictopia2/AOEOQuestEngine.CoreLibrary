namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class LocalizedStringManager
{
    int _value = 0;
    public async Task ResetAsync()
    {
        _value = 80000;
        _strings.Clear();
        string oldPath = dd1.RawStringTableLocation;
        string newPath = dd1.NewStringTableLocation;
        await ff1.FileCopyAsync(oldPath, newPath);
        Startup();
    }
    //eventually can improve this.
    private static BasicList<XElement> _strings = [];
    private XElement? _source;
    private void Startup()
    {
        if (_strings.Count > 0)
        {
            return;
        }
        _source = XElement.Load(dd1.NewStringTableLocation);
        _strings = _source.Descendants("language").Descendants("string").ToBasicList();
        if (_strings.Count == 0)
        {
            throw new CustomBasicException("Must have at least one string");
        }
    }

    public string InsertLocalizedString(string value)
    {
        if (_value == 0)
        {
            throw new CustomBasicException("Needs to reset first");
        }
        _value++;

        // Replace all kinds of line breaks with literal " \n"
        string sanitized = value.Replace("\r\n", " \\n")
                                .Replace("\n", " \\n")
                                .Replace("\r", " \\n");

        XElement newElement = new ("string", sanitized);
        newElement.SetAttributeValue("_locid", _value.ToString());

        // Append to the language node
        XElement? languageNode = (_source?.Element("language")) ?? throw new CustomBasicException("Language node was not found in the XML.");
        languageNode.Add(newElement);

        // Optionally keep the in-memory list updated too
        _strings.Add(newElement);

        // Save the updated XML back to file
        _source?.Save(dd1.NewStringTableLocation);
        return _value.ToString();
    }
}