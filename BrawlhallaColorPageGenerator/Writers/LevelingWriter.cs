using System.Collections.Generic;
using System.IO;
using System.Linq;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers;

public sealed class LevelingWriter(WriterData data)
{
    private static readonly string[] LEVELING_COLORS = [
        "Blue",
        "Yellow",
        "Green",
        "Brown",
        "Orange",
        "Purple",
        "Cyan",
        "Sunset",
        "Grey",
        "Pink",
        "Red",
    ];

    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine(
"""
<includeonly><onlyinclude>
{{#switch: {{lc:{{{1}}}}}
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

            writer.Write(" = {{LegendLevelingRow|");
            writer.Write(hero.BioName);

            int stanceNumber = 1;
            foreach (RuneType rune in runes)
            {
                string? name = rune.ShortName;
                if (name is null) continue;
                writer.Write("|stance");
                writer.Write(stanceNumber++);
                writer.Write('=');
                writer.Write(name);
            }

            HashSet<string> colorRewardsSet = [.. hero.ColorRewards];
            string[] leftoverColors = [.. LEVELING_COLORS.Where((c) => !colorRewardsSet.Contains(c))];
            IEnumerable<string> colors = leftoverColors.Concat(hero.ColorRewards);

            int colorNumber = 1;
            foreach (string color in colors)
            {
                writer.Write("|color");
                writer.Write(colorNumber++);
                writer.Write('=');
                writer.Write(color.ToLower());
            }
            writer.WriteLine("}}");
        }
        writer.WriteLine(
"""
}}
</onlyinclude></includeonly><noinclude>
{| class="wikitable" style="text-align:center;"
|-
{{LegendLevelingRowByName|Bodvar}}
|-
{{LegendLevelingRowByName|Lady Vera}}
|}

[[Category:Templates]]</noinclude>
"""
);
    }
}