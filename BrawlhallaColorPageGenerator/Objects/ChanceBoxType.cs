using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class ChanceBoxType
{
    public string ChanceBoxName { get; }
    public string DisplayNameKey { get; }
    public string[] ExclusiveItems { get; }

    public ChanceBoxType(XElement element)
    {
        ChanceBoxName = element.Attribute(nameof(ChanceBoxName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))!.Value;
        ExclusiveItems = element.Element(nameof(ExclusiveItems))?.Value.Split(',') ?? [];
    }
}

public sealed class ChanceBoxTypes
{
    public ChanceBoxType[] ChanceBoxes { get; }
    public Dictionary<string, ChanceBoxType> ExclusiveItemToChanceBox { get; }

    public ChanceBoxTypes(string content)
    {
        XElement element = XElement.Parse(content);
        ChanceBoxes = [.. element.Elements(nameof(ChanceBoxType)).Select((e) => new ChanceBoxType(e))];
        ExclusiveItemToChanceBox = ChanceBoxes
            .Where((cb) => cb.ChanceBoxName != "Template")
            .SelectMany((cb) => cb.ExclusiveItems.Select((item) => (item, cb)))
            .ToDictionary((x) => x.item, (x) => x.cb);
    }
}