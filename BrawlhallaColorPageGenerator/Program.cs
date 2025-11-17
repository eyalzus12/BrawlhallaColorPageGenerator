using System;
using System.Collections.Generic;
using System.IO;
using BrawlhallaColorPageGenerator;
using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaColorPageGenerator.Writers;
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

#region File loading

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
LangFile langFile;
using (FileStream file = File.OpenRead(lang))
    langFile = LangFile.Load(file);

#endregion
#region Parsing

// Costumes
string costumeTypesContent = files["costumeTypes.csv"];
CostumeTypes costumeTypes = new(costumeTypesContent);
Array.Sort(costumeTypes.Costumes, Comparer<CostumeType>.Create((a, b) =>
{
    if (a.OwnerHero != b.OwnerHero) return string.Compare(a.OwnerHero, b.OwnerHero);

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

// Weapon skins
string weaponSkinTypesContent = files["weaponSkinTypes.csv"];
WeaponSkinTypes weaponSkinTypes = new(weaponSkinTypesContent);
Array.Sort(weaponSkinTypes.WeaponSkins, Comparer<WeaponSkinType>.Create((a, b) =>
{
    if (a.BaseWeapon != b.BaseWeapon) return string.Compare(
        Utils.BASE_WEAPON_NAME[a.BaseWeapon],
        Utils.BASE_WEAPON_NAME[b.BaseWeapon]
    );

    if (a.DisplayNameKey == b.DisplayNameKey)
    {
        int upgradeLevelA = weaponSkinTypes.UpgradeLevel.GetValueOrDefault(a.WeaponSkinName, 0);
        int upgradeLevelB = weaponSkinTypes.UpgradeLevel.GetValueOrDefault(b.WeaponSkinName, 0);
        return upgradeLevelA.CompareTo(upgradeLevelB);
    }
    else
    {
        string aName = langFile.Entries.GetValueOrDefault(a.DisplayNameKey ?? "", "~" + a.WeaponSkinName);
        string bName = langFile.Entries.GetValueOrDefault(b.DisplayNameKey ?? "", "~" + b.WeaponSkinName);
        return string.Compare(aName, bName);
    }
}));

// Companions
string companionTypesContent = files["CompanionTypes.xml"];
CompanionTypes companionTypes = new(companionTypesContent);
Array.Sort(companionTypes.Companions, Comparer<CompanionType>.Create((a, b) =>
{
    string aName = langFile.Entries.GetValueOrDefault(a.DisplayNameKey, "~" + a.CompanionName);
    string bName = langFile.Entries.GetValueOrDefault(b.DisplayNameKey, "~" + b.CompanionName);
    return string.Compare(aName, bName);
}));

// Heros
string heroTypesContent = files["HeroTypes.xml"];
HeroTypes heroTypes = new(heroTypesContent);
Array.Sort(heroTypes.Heroes, Comparer<HeroType>.Create((a, b) =>
{
    return string.Compare(a.BioName, b.BioName);
}));

#endregion
#region Writing

SkinsWriter skinsWriter = new(heroTypes, costumeTypes, langFile);
skinsWriter.WriteTo("skins.txt");

WeaponSkinsWriter weaponSkinsWriter = new(weaponSkinTypes, langFile);
weaponSkinsWriter.WriteTo("weaponSkins.txt");

CompanionsWriter companionsWriter = new(companionTypes, langFile);
companionsWriter.WriteTo("companions.txt");

#endregion