using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class CompanionType
{
    public string CompanionName { get; set; }
    public string DisplayNameKey { get; set; }

    public CompanionType(XElement element)
    {
        CompanionName = element.Attribute("CompanionName")!.Value;
        DisplayNameKey = element.Element("DisplayNameKey")!.Value;
    }
}

public sealed class CompanionTypes
{
    public CompanionType[] Companions { get; set; }

    public CompanionTypes(string content)
    {
        XElement element = XElement.Parse(content);
        Companions = [.. element.Elements("CompanionType").Select((e) => new CompanionType(e))];
    }
}