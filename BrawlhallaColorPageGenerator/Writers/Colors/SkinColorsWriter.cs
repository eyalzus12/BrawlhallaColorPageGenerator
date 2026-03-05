using System;
using System.Globalization;
using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers.Colors;

public sealed class SkinColorsWriter(WriterData data)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine("<includeonly>");
        writer.WriteLine("The following is a list of all skins in {{{1|}}}. ''Click an image to view it in higher resolution.''");
        writer.WriteLine();
        writer.WriteLine("{{Compact TOC}}");
        char currentLetter = '~';
        foreach (HeroType hero in data.HeroTypes.Heroes)
        {
            if (!hero.IsActive || hero.HeroName == "Random") continue;

            ArgumentNullException.ThrowIfNull(hero.BioName);
            string name = hero.BioName;
            char firstLetter = name[0];
            if (currentLetter != firstLetter)
            {
                currentLetter = firstLetter;
                writer.Write("<span id=\"");
                writer.Write(currentLetter);
                writer.WriteLine("\"></span>");
            }

            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            string titleCaseName = textInfo.ToTitleCase(name);
            writer.Write("===[[");
            writer.Write(titleCaseName);
            writer.WriteLine("]]===");
            writer.WriteLine("{{List to itembox|color={{{1|}}}|");
            foreach (CostumeType costumeType in data.CostumeTypes.Costumes)
            {
                if (
                    costumeType.OwnerHero != hero.HeroName || // not my hero
                    costumeType.CostumeName.StartsWith("ZombieWalker") ||
                    costumeType.CostumeName.EndsWith("Stance2")
                ) continue;

                (string costumeName, string imageName, string displayName, _) = data.GetSkinNameParams(costumeType, true);

                writer.Write(costumeName);
                if (costumeName != displayName)
                {
                    writer.Write(" && displayname:");
                    writer.Write(displayName);
                }
                if (costumeName != imageName)
                {
                    writer.Write(" && image:");
                    writer.Write(imageName);
                    writer.Write(" $1.png");
                }
                writer.WriteLine();
            }
            writer.WriteLine("}}");
        }
        writer.WriteLine("[[Category:Skins in all colors]]</includeonly>");
        writer.WriteLine("<noinclude>");
        writer.WriteLine("{{doc}}");
        writer.WriteLine("</noinclude>");
    }
}