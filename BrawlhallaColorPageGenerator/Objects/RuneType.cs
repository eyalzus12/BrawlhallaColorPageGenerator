using System.Collections.Generic;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class RuneType
{
    public string IconName { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Weight { get; set; }
    public int Speed { get; set; }

    public RuneType(XElement element)
    {
        IconName = element.Element(nameof(IconName))!.Value;
        Strength = int.Parse(element.Element(nameof(Strength))!.Value);
        Dexterity = int.Parse(element.Element(nameof(Dexterity))!.Value);
        Weight = int.Parse(element.Element(nameof(Weight))!.Value);
        Speed = int.Parse(element.Element(nameof(Speed))!.Value);
    }

    public string? ShortName => IconName switch
    {
        "a_StanceIcon_Strength" => "str",
        "a_StanceIcon_Dexterity" => "dex",
        "a_StanceIcon_Weight" => "def",
        "a_StanceIcon_Speed" => "spd",
        _ => null,
    };

    public string TakesFrom(int str, int dex, int def, int spd) =>
        Strength == str - 1 ? "str"
        : Dexterity == dex - 1 ? "dex"
        : Weight == def - 1 ? "def"
        : Speed == spd - 1 ? "spd"
        : "err";
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