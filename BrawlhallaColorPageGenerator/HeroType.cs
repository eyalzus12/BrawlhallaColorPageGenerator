using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator;

public sealed class HeroType
{
    public string HeroName { get; set; }
    public string? BioName { get; set; }
    public string CostumeName { get; set; }
    public bool IsActive { get; set; }

    public HeroType(XElement element)
    {
        HeroName = element.Attribute("HeroName")!.Value;
        BioName = element.Element("BioName")?.Value;
        CostumeName = element.Element("CostumeName")!.Value;
        IsActive = string.Equals(element.Element("IsActive")?.Value, "TRUE", System.StringComparison.InvariantCultureIgnoreCase);
    }
}

public sealed class HeroTypes
{
    public HeroType[] Heroes { get; set; }

    public HeroTypes(string content)
    {
        XElement element = XElement.Parse(content);
        Heroes = [.. element.Elements("HeroType").Select((e) => new HeroType(e))];
    }
}