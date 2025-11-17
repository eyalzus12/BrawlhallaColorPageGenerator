using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using BrawlhallaColorPageGenerator;
using BrawlhallaLangReader;
using BrawlhallaSwz;

const string BRAWLHALLA_FOLDER = "C:/Program Files (x86)/Steam/steamapps/common/Brawlhalla";

uint swzKey;
if (args.Length < 1)
{
    Console.WriteLine("Please insert the swz key");
    swzKey = uint.Parse(Console.ReadLine()!);
}
else
{
    swzKey = uint.Parse(args[1]);
}

string gameSwz = Path.Combine(BRAWLHALLA_FOLDER, "Game.swz");
Dictionary<string, string> files = [];
using (FileStream file = File.OpenRead(gameSwz))
{
    using SwzReader swzReader = new(file, swzKey);
    foreach (string fileContent in swzReader.ReadFiles())
    {
        string fileName = SwzUtils.GetFileName(fileContent);
        files[fileName] = fileContent;
    }
}

string lang = Path.Combine(BRAWLHALLA_FOLDER, "languages", "language.1.bin");
Dictionary<string, string> langEntries;
using (FileStream file = File.OpenRead(lang))
{
    langEntries = LangFile.Load(file).Entries;
}

string costumeTypesContent = files["costumeTypes.csv"];
CostumeTypes costumeTypes = new(costumeTypesContent);
Array.Sort(costumeTypes.Costumes, Comparer<CostumeType>.Create((a, b) =>
{
    if (a.OwnerHero != b.OwnerHero) return string.Compare(a.OwnerHero, b.OwnerHero);

    // string? nameA = a.DisplayNameKey is null ? null : langEntries.GetValueOrDefault(a.DisplayNameKey, null!);
    // string? nameB = b.DisplayNameKey is null ? null : langEntries.GetValueOrDefault(b.DisplayNameKey, null!);
    if (a.DisplayNameKey == b.DisplayNameKey)
    {
        int upgradeLevelA = costumeTypes.UpgradeLevel.GetValueOrDefault(a.CostumeName, 0);
        int upgradeLevelB = costumeTypes.UpgradeLevel.GetValueOrDefault(b.CostumeName, 0);
        return upgradeLevelA.CompareTo(upgradeLevelB);
    }
    else
    {
        return a.CostumeIndex.CompareTo(b.CostumeIndex);
    }
}));

string heroTypesContent = files["HeroTypes.xml"];
HeroTypes heroTypes = new(heroTypesContent);
Array.Sort(heroTypes.Heroes, Comparer<HeroType>.Create((a, b) =>
{
    return string.Compare(a.BioName, b.BioName);
}));

using StreamWriter skinsWriter = new("skins.txt");
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

        string costumeName = costumeType.DisplayNameKey is null ? titleCaseName : langEntries[costumeType.DisplayNameKey];

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
