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
                skinsWriter.WriteLine("<span id=\"¹\"></span>".Apply(currentLetter));
            }

            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            string titleCaseName = textInfo.ToTitleCase(name);
            skinsWriter.WriteLine("===[[¹]]===".Apply(titleCaseName));
            skinsWriter.WriteLine("{{itembox/top}}");
            foreach (CostumeType costumeType in costumeTypes.Costumes)
            {
                if (
                    costumeType.OwnerHero != hero.HeroName || // not my hero
                    costumeType.CostumeName.StartsWith("ZombieWalker") ||
                    costumeType.CostumeName.EndsWith("Stance2")
                ) continue;

                (string costumeName, string imageName, string displayName) = GetNameParams(costumeType, titleCaseName);

                skinsWriter.WriteLine("{{itembox|width=150|height=150|name=¹²|image=³ {{{1|}}}.png|compact=true|noimglink=true}}".Apply3(
                    costumeName,
                    costumeName == displayName ? "" : ("|displayname=" + displayName),
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

    private (string skinName, string imageName, string displayName) GetNameParams(CostumeType costumeType, string nameDefault)
    {
        string costumeName = costumeType.CostumeName;
        string? displayNameKey = costumeType.DisplayNameKey;

        string skinName = displayNameKey is null ? nameDefault : langFile.Entries[displayNameKey];
        string imageName = skinName;
        string displayName = skinName;

        switch (costumeName)
        {
            case "SnakeEyes":
                imageName = "Snake Eyes (Thatch Skin)";
                break;
            case "Eivor":
                displayName = imageName = "Eivor (Female)";
                break;
            case "EivorMale":
                displayName = imageName = "Eivor (Male)";
                break;
        }

        if (costumeTypes.UpgradeLevel.TryGetValue(costumeName, out int upgradeLevel) && upgradeLevel != 0)
        {
            displayName = skinName + " (Lvl " + upgradeLevel + ")";
            imageName = skinName + " Level " + upgradeLevel;
        }

        imageName = imageName.Replace(":", "");

        return (skinName, imageName, displayName);
    }
}