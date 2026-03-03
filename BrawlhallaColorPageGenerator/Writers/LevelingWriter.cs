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
        foreach (HeroType hero in data.HeroTypes.Heroes)
        {
            if (!data.RuneTypes.HeroRunes.TryGetValue(hero.HeroName, out var runes))
                continue;

            writer.Write("|- {{LegendLevelingRow|");
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
    }
}