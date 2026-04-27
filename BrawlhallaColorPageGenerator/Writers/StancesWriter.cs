using System.IO;
using System.Linq;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers;

public sealed class StancesWriter(WriterData data)
{
    private static readonly int[] LEVELS_FOR_INDEX = [0, 3, 4, 6, 8];

    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine(
"""
<includeonly><onlyinclude>
{{#switch:{{lc:{{{1}}}}}
"""
        );
        foreach (HeroType hero in data.HeroTypes.Heroes.OrderBy((h) => h.ReleaseOrderID))
        {
            if (!data.RuneTypes.HeroRunes.TryGetValue(hero.HeroName, out var runes) || hero.BioName is null)
                continue;

            writer.Write('|');
            writer.Write(hero.BioName.ToLowerInvariant());
            if (hero.HeroName == "Viking")
                writer.Write("|bodvar");

            writer.Write(" = {{LegendStancesRow|{{#ifeq:{{{nohead}}}|true||");
            writer.Write(hero.BioName);
            writer.Write("}}|str=");
            writer.Write(hero.Strength);
            writer.Write("|dex=");
            writer.Write(hero.Dexterity);
            writer.Write("|def=");
            writer.Write(hero.Weight);
            writer.Write("|spd=");
            writer.Write(hero.Speed);

            int strStanceIndex = runes.FindIndex((r) => r.ShortName == "str");
            RuneType strStance = runes[strStanceIndex];
            string strTakeFrom = strStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|str_take=");
            writer.Write(strTakeFrom);

            int dexStanceIndex = runes.FindIndex((r) => r.ShortName == "dex");
            RuneType dexStance = runes[dexStanceIndex];
            string dexTakeFrom = dexStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|dex_take=");
            writer.Write(dexTakeFrom);

            int defStanceIndex = runes.FindIndex((r) => r.ShortName == "def");
            RuneType defStance = runes[defStanceIndex];
            string defTakeFrom = defStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|def_take=");
            writer.Write(defTakeFrom);

            int spdStanceIndex = runes.FindIndex((r) => r.ShortName == "spd");
            RuneType spdStance = runes[spdStanceIndex];
            string spdTakeFrom = spdStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|spd_take=");
            writer.Write(spdTakeFrom);

            writer.Write("|levels={{{levels|}}}|str_level=");
            writer.Write(LEVELS_FOR_INDEX[strStanceIndex]);
            writer.Write("|dex_level=");
            writer.Write(LEVELS_FOR_INDEX[dexStanceIndex]);
            writer.Write("|def_level=");
            writer.Write(LEVELS_FOR_INDEX[defStanceIndex]);
            writer.Write("|spd_level=");
            writer.Write(LEVELS_FOR_INDEX[spdStanceIndex]);

            writer.WriteLine("}}");
        }
        writer.WriteLine(
"""
}}</onlyinclude></includeonly><noinclude>
{| class="wikitable" style="text-align:center;"
{{LegendStancesRowByName|Bodvar}}
{{LegendStancesRowByName|Lady Vera}}
|}

{| class="wikitable" style="text-align:center;"
{{LegendStancesRowByName|Lady Vera|nohead=true}}
|}

{| class="wikitable" style="text-align:center;"
{{LegendStancesRowByName|Lady Vera|nohead=true|levels=true}}
|}

[[Category:Templates]]</noinclude>
"""
        );
    }
}