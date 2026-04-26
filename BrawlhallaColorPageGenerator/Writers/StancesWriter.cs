using System.IO;
using System.Linq;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers;

public sealed class StancesWriter(WriterData data)
{
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

            RuneType strStance = runes.First((r) => r.ShortName == "str");
            string strTakeFrom = strStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|str_take=");
            writer.Write(strTakeFrom);

            RuneType dexStance = runes.First((r) => r.ShortName == "dex");
            string dexTakeFrom = dexStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|dex_take=");
            writer.Write(dexTakeFrom);

            RuneType defStance = runes.First((r) => r.ShortName == "def");
            string defTakeFrom = defStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|def_take=");
            writer.Write(defTakeFrom);

            RuneType spdStance = runes.First((r) => r.ShortName == "spd");
            string spdTakeFrom = spdStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|spd_take=");
            writer.Write(spdTakeFrom);

            writer.WriteLine("}}");
        }
        writer.WriteLine(
"""
}}</onlyinclude></includeonly><noinclude>
{| class="wikitable" style="text-align:center;"
|-
{{LegendStancesRowByName|Bodvar}}
|-
{{LegendStancesRowByName|Lady Vera}}
|}
{| class="wikitable" style="text-align:center;"
|-
{{LegendStancesRowByName|Lady Vera|nohead=true}}
|}

[[Category:Templates]]</noinclude>
"""
        );
    }
}