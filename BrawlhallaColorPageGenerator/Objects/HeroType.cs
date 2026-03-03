using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class HeroType
{
    public string HeroName { get; }
    public string? BioName { get; }
    public string CostumeName { get; }
    public string[] ColorRewards { get; }
    public bool IsActive { get; }
    public string? BaseWeapon1 { get; }
    public string? BaseWeapon2 { get; }

    public HeroType(XElement element)
    {
        HeroName = element.Attribute(nameof(HeroName))!.Value;
        BioName = element.Element(nameof(BioName))?.Value;
        CostumeName = element.Element(nameof(CostumeName))!.Value;
        ColorRewards = element.Element(nameof(ColorRewards))?.Value.Split(',') ?? [];
        IsActive = string.Equals(element.Element(nameof(IsActive))?.Value, "TRUE", System.StringComparison.InvariantCultureIgnoreCase);
        BaseWeapon1 = element.Element(nameof(BaseWeapon1))?.Value;
        BaseWeapon2 = element.Element(nameof(BaseWeapon2))?.Value;
    }
}

public sealed class HeroTypes
{
    public HeroType[] Heroes { get; }
    public Dictionary<string, HeroType> HeroesMap { get; }

    public HeroTypes(string content)
    {
        XElement element = XElement.Parse(content);
        Heroes = [.. element.Elements(nameof(HeroType)).Select((e) => new HeroType(e))];
        HeroesMap = Heroes.ToDictionary((h) => h.HeroName);
    }
}