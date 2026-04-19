using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class ColorSchemeType
{
    public string ColorSchemeName { get; }
    public string DisplayNameKey { get; }
    public string[] ExcludeOpponentTeamColor { get; }
    public string? FallbackOpponentTeamColor { get; }
    public string? FallbackMyTeamColor { get; }

    public ColorSchemeType(XElement element)
    {
        ColorSchemeName = element.Attribute(nameof(ColorSchemeName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))!.Value;
        ExcludeOpponentTeamColor = element.Element(nameof(ExcludeOpponentTeamColor))?.Value.Split(',') ?? [];
        FallbackOpponentTeamColor = element.Element(nameof(FallbackOpponentTeamColor))?.Value;
        FallbackMyTeamColor = element.Element(nameof(FallbackMyTeamColor))?.Value;
    }
}

public sealed class ColorSchemeTypes
{
    public ColorSchemeType[] ColorSchemes { get; }
    public Dictionary<string, ColorSchemeType> ColorSchemesMap { get; }

    public ColorSchemeTypes(string content)
    {
        XElement element = XElement.Parse(content);
        ColorSchemes = [.. element.Elements(nameof(ColorSchemeType)).Select((e) => new ColorSchemeType(e))];
        ColorSchemesMap = ColorSchemes.ToDictionary((c) => c.ColorSchemeName);
    }
}