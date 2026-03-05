using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class EntitlementType
{
    public string EntitlementName { get; set; }
    public string? DisplayNameKey { get; set; }
    public string[] Costumes { get; set; }
    public string[] WeaponSkins { get; set; }

    public EntitlementType(XElement element)
    {
        EntitlementName = element.Attribute(nameof(EntitlementName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))?.Value;
        Costumes = element.Element(nameof(Costumes))?.Value.Split(',') ?? [];
        WeaponSkins = element.Element(nameof(WeaponSkins))?.Value.Split(',') ?? [];
    }
}

public sealed class EntitlementTypes
{
    public EntitlementType[] Entitlements { get; set; }
    public Dictionary<string, EntitlementType> CostumeToEntitlement { get; set; }
    public Dictionary<string, EntitlementType> WeaponSkinToEntitlement { get; set; }

    public EntitlementTypes(string content)
    {
        XElement element = XElement.Parse(content);
        Entitlements = [.. element.Elements(nameof(EntitlementType)).Select((e) => new EntitlementType(e))];
        CostumeToEntitlement = Entitlements
            .Where((e) => e.EntitlementName != "Template")
            .SelectMany((e) => e.Costumes.Select((w) => (w, e)))
            .ToDictionary((x) => x.w, (x) => x.e);
        WeaponSkinToEntitlement = Entitlements
            .Where((e) => e.EntitlementName != "Template")
            .SelectMany((e) => e.WeaponSkins.Select((w) => (w, e)))
            .ToDictionary((x) => x.w, (x) => x.e);
    }
}