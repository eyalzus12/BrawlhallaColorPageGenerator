using System.Collections.Generic;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class RuneType
{
    public string IconName { get; set; }

    public RuneType(XElement element)
    {
        IconName = element.Element("IconName")!.Value;
    }

    public string? ShortName => IconName switch
    {
        "a_StanceIcon_Strength" => "str",
        "a_StanceIcon_Speed" => "spd",
        "a_StanceIcon_Dexterity" => "dex",
        "a_StanceIcon_Weight" => "def",
        _ => null,
    };
}

public sealed class RuneTypes
{
    public Dictionary<string, List<RuneType>> HeroRunes { get; } = [];

    public RuneTypes(string content)
    {
        string heroName = null!;
        XElement element = XElement.Parse(content);
        foreach (XElement rune in element.Elements())
        {
            string? newHeroName = rune.Element("HeroName")?.Value;
            if (newHeroName is not null) heroName = newHeroName;

            RuneType runeType = new(rune);

            HeroRunes.TryAdd(heroName, []);
            HeroRunes[heroName].Add(runeType);
        }
    }
}