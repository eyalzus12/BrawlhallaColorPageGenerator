using System;
using System.Globalization;
using System.IO;
using BrawlhallaLangReader;

namespace BrawlhallaColorPageGenerator.Writers;

public sealed class SkinsWriter(HeroTypes heroTypes, CostumeTypes costumeTypes, LangFile langFile)
{
    public void WriteTo(string path)
    {
        using StreamWriter skinsWriter = new(path);
        skinsWriter.WriteLine("<includeonly>");
        skinsWriter.WriteLine("The following is a list of all skins in {{{1|}}}. ''Click an image to view it in higher resolution.''");
        skinsWriter.WriteLine();
        skinsWriter.WriteLine("{{Compact TOC}}");
        char currentLetter = '~';
        foreach (HeroType hero in heroTypes.Heroes)
        {
            if (!hero.IsActive || hero.HeroName == "Random") continue;

            ArgumentNullException.ThrowIfNull(hero.BioName);
            string name = hero.BioName;
            char firstLetter = name[0];
            if (currentLetter != firstLetter)
            {
                currentLetter = firstLetter;
                skinsWriter.WriteLine("<span id=\"@\"></span>".Apply(currentLetter));
            }

            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            string titleCaseName = textInfo.ToTitleCase(name);
            skinsWriter.WriteLine("===[[@]]===".Apply(titleCaseName));
            skinsWriter.WriteLine("{{itembox/top}}");
            foreach (CostumeType costumeType in costumeTypes.Costumes)
            {
                if (
                    costumeType.OwnerHero != hero.HeroName || // not my hero
                    costumeType.CostumeName.StartsWith("ZombieWalker") ||
                    costumeType.CostumeName.EndsWith("Stance2")
                ) continue;

                string costumeName = costumeType.DisplayNameKey is null ? titleCaseName : langFile.Entries[costumeType.DisplayNameKey];

                string imageName = costumeName;
                if (costumeType.CostumeName == "SnakeEyes")
                    imageName = "Snake Eyes (Thatch Skin)";

                string displayName = "";
                if (costumeType.CostumeName == "Eivor")
                {
                    displayName = "Eivor (Female)";
                    imageName = "Eivor (Female)";
                }
                else if (costumeType.CostumeName == "EivorMale")
                {
                    displayName = "Eivor (Male)";
                    imageName = "Eivor (Male)";
                }

                if (costumeTypes.UpgradeLevel.TryGetValue(costumeType.CostumeName, out int upgradeLevel) && upgradeLevel != 0)
                {
                    displayName = costumeName + " (Lvl " + upgradeLevel + ")";
                    imageName = costumeName + " Level " + upgradeLevel;
                }

                skinsWriter.WriteLine("{{itembox|width=150|height=150|name=@#|image=& {{{1|}}}.png|compact=true|noimglink=true}}".Apply3(
                    costumeName,
                    string.IsNullOrEmpty(displayName) ? "" : ("|displayname=" + displayName),
                    imageName
                ));
            }
            skinsWriter.WriteLine("{{itembox/bottom}}");
        }
        skinsWriter.WriteLine("[[Category:Skins in all colors]]</includeonly>");
        skinsWriter.WriteLine("<noinclude>");
        skinsWriter.WriteLine("{{doc}}");
        skinsWriter.WriteLine("</noinclude>");

    }
}