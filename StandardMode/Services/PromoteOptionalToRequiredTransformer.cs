namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public class PromoteOptionalToRequiredTransformer : ISecondaryObjectiveTransformer
{
    public void Transform(XElement questXml)
    {
        //try with quest BFK_M201_VaitaraniRiver_Legendary
        var required = questXml.Element("objectives") ?? throw new InvalidOperationException("Missing required <objectives> section.");

        // Collect existing groups (direct children of <values> in the required objectives)
        var existingGroups = new HashSet<string>(
            required.Descendants("values")
                    .Elements()
                    .Select(e => e.ToString(SaveOptions.DisableFormatting))
        );

        // Get max id to assign new unique IDs
        int maxId = required.DescendantsAndSelf()
            .Where(e => e.Attribute("id") != null)
            .Select(e => (int?)int.Parse(e.Attribute("id")!.Value))
            .Max() ?? 0;

        var secondaryBlocks = questXml.Elements("secondaryobjectives").ToList();
        foreach (var secondary in secondaryBlocks)
        {
            var orBlock = secondary.Element("values")?.Element("or");
            if (orBlock == null)
            {
                continue;
            }

            // These are the main challenge groups inside secondary objectives
            foreach (var challengeGroup in orBlock.Element("values")?.Elements() ?? [])
            {
                string serializedGroup = challengeGroup.ToString(SaveOptions.DisableFormatting);

                if (!existingGroups.Contains(serializedGroup))
                {
                    XElement clonedGroup = new (challengeGroup);
                    UpdateIdsRecursively(clonedGroup, ref maxId);
                    AddChallengeGroupToRequired(required, clonedGroup, ref maxId);
                    existingGroups.Add(clonedGroup.ToString(SaveOptions.DisableFormatting));
                }
            }

            secondary.Remove();
        }
    }

    private static void AddChallengeGroupToRequired(XElement requiredObjectives, XElement challengeGroup, ref int maxId)
    {
        // Ensure <values> exists in <objectives>
        var valuesNode = requiredObjectives.Element("values");
        if (valuesNode == null)
        {
            valuesNode = new XElement("values");
            requiredObjectives.Add(valuesNode);
        }

        // Ensure top-level <or> block exists inside <values>
        var orNode = valuesNode.Element("or");
        if (orNode == null)
        {
            orNode = new XElement("or", new XAttribute("id", ++maxId), new XAttribute("mustfailall", "false"));
            orNode.Add(new XElement("values"));
            valuesNode.Add(orNode);
        }

        var orValuesNode = orNode.Element("values")!;
        if (orValuesNode == null)
        {
            orValuesNode = new XElement("values");
            orNode.Add(orValuesNode);
        }

        // Wrap the challenge group inside <and> block if it isn't already <and>
        if (challengeGroup.Name != "and")
        {
            var andWrapper = new XElement("and", new XAttribute("id", ++maxId), new XAttribute("mustfailall", "false"));
            andWrapper.Add(new XElement("values", challengeGroup));
            orValuesNode.Add(andWrapper);
        }
        else
        {
            orValuesNode.Add(challengeGroup);
        }
    }

    private static void UpdateIdsRecursively(XElement element, ref int maxId)
    {
        element.SetAttributeValue("id", ++maxId);
        foreach (var child in element.Elements())
        {
            UpdateIdsRecursively(child, ref maxId);
        }
    }
}