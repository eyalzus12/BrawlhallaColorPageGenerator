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
    // string? nameA = a.DisplayNameKey is null ? null : langEntries.GetValueOrDefault(a.DisplayNameKey, null!);
    // string? nameB = b.DisplayNameKey is null ? null : langEntries.GetValueOrDefault(b.DisplayNameKey, null!);
    return a.CostumeIndex.CompareTo(b.CostumeIndex);
}));

string heroTypesContent = files["HeroTypes.xml"];
HeroTypes heroTypes = new(heroTypesContent);
Array.Sort(heroTypes.Heroes, Comparer<HeroType>.Create((a, b) =>
{
    return string.Compare(a.BioName, b.BioName);
}));

using StreamWriter writer = new("output.txt");
writer.WriteLine("<includeonly>");
writer.WriteLine("The following is a list of all skins in {{{1|}}}. ''Click an image to view it in higher resolution.''");
writer.WriteLine();
writer.WriteLine("{{Compact TOC}}");
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
        writer.WriteLine("<span id=\"@\"></span>".Apply(currentLetter));
    }

    TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
    string titleCaseName = textInfo.ToTitleCase(name);
    writer.WriteLine("===[[@]]===".Apply(titleCaseName));
    writer.WriteLine("{{Itembox/top}}");
    writer.WriteLine("{{itembox|width=150|height=150|name=@|image=@ {{{1|}}}.png|compact=true|noimglink=true}}".Apply(titleCaseName));
    foreach (CostumeType costumeType in costumeTypes.Costumes)
    {
        if (costumeType.OwnerHero != hero.HeroName || costumeType.DisplayNameKey is null) continue;
        string costumeName = langEntries[costumeType.DisplayNameKey];
        writer.WriteLine("{{itembox|width=150|height=150|name=@|image=@ {{{1|}}}.png|compact=true|noimglink=true}}".Apply(costumeName));
    }
    writer.WriteLine("{{Itembox/bottom}}");
}

writer.WriteLine("[[Category:Skins in all colors]]</includeonly>");
writer.WriteLine("<noinclude>");
writer.WriteLine("{{doc}}");
writer.WriteLine("</noinclude>");
