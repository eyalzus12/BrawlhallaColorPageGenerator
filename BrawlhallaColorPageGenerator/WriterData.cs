using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaLangReader;

namespace BrawlhallaColorPageGenerator;

public class WriterData
{
    public required CostumeTypes CostumeTypes { get; init; }
    public required WeaponSkinTypes WeaponSkinTypes { get; init; }
    public required HeroTypes HeroTypes { get; init; }
    public required RuneTypes RuneTypes { get; init; }
    public required CompanionTypes CompanionTypes { get; init; }
    public required StoreTypes StoreTypes { get; init; }
    public required LangFile LangFile { get; init; }

    public Dictionary<string, CostumeType>? WeaponSkinToCostume { get; private set; } = null;

    [MemberNotNull(nameof(WeaponSkinToCostume))]
    private void InitWeaponSkinToCostume()
    {
        if (WeaponSkinToCostume is not null) return;
        WeaponSkinToCostume = [];
        foreach (CostumeType costume in CostumeTypes.Costumes)
        {
            if (costume.WeaponSet is null || costume.DoesNotOwnWeaponSet || !HeroTypes.HeroesMap.TryGetValue(costume.OwnerHero, out HeroType? hero))
                continue;

            if (hero.BaseWeapon1 is not null)
                WeaponSkinToCostume[hero.BaseWeapon1 + costume.WeaponSet] = costume;
            if (hero.BaseWeapon2 is not null)
                WeaponSkinToCostume[hero.BaseWeapon2 + costume.WeaponSet] = costume;
        }
    }

    public (string skinName, string imageName, string displayName) GetSkinNameParams(CostumeType costumeType)
    {
        string costumeName = costumeType.CostumeName;
        string? displayNameKey = costumeType.DisplayNameKey;

        string skinName;
        if (displayNameKey is not null)
        {
            skinName = LangFile.Entries[displayNameKey];
        }
        else
        {
            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            string ownerHero = costumeType.OwnerHero;
            HeroType hero = HeroTypes.HeroesMap[ownerHero];
            ArgumentNullException.ThrowIfNull(hero.BioName);
            skinName = textInfo.ToTitleCase(hero.BioName);
        }

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

        if (CostumeTypes.UpgradeLevel.TryGetValue(costumeName, out int upgradeLevel) && upgradeLevel != 0)
        {
            displayName = skinName + " (Lvl " + upgradeLevel + ")";
            imageName = skinName + " Level " + upgradeLevel;
        }

        skinName = skinName.Replace(":", "&#58;");
        displayName = displayName.Replace(":", "&#58;");
        imageName = imageName.Replace(":", "");

        return (skinName, imageName, displayName);
    }

    public (string weaponSkinName, string imageName, string displayName) GetWeaponSkinNameParams(WeaponSkinType weaponSkinType)
    {
        string weaponSkin = weaponSkinType.WeaponSkinName;
        string displayNameKey = weaponSkinType.DisplayNameKey!;

        string weaponSkinName = LangFile.Entries[displayNameKey];
        string imageName = weaponSkinName;
        string displayName = weaponSkinName;

        switch (weaponSkin)
        {
            case "AxeSimon":
                displayName = imageName = weaponSkinName = "Battle Axe (Simon Belmont)";
                break;
            case "AxeGilded":
                weaponSkinName = "Gilded Glory (Axe Skin)";
                break;
            case "AxeActualValk":
                weaponSkinName = "Glory (Weapon Skin)";
                break;
            case "PistolSerape":
                weaponSkinName = "Snake Eyes (Weapon Skin)";
                imageName = weaponSkinName;
                break;
            case "BowOldKoji":
                weaponSkinName = "Heirloom (Bow Skin)";
                break;
            case "CannonDestinyTitan":
                imageName = "Dragon's Breath (Titan)";
                break;
            case "FistsVolcano":
                weaponSkinName = "Hot Lava (Weapon Skin)";
                break;
            case "FistsOrb4":
                weaponSkinName = "Knockouts (Weapon Skin)";
                break;
            case "HammerMadame":
                imageName = weaponSkinName = "Heirloom (Hammer Skin)";
                break;
            case "RocketLanceMotorcycle":
                weaponSkinName = "Burnout (Weapon Skin)";
                break;
            case "SpearGem":
                imageName = weaponSkinName = "Dusk (Weapon Skin)";
                break;
            case "SpearViral":
                weaponSkinName = "Vector (Weapon Skin)";
                imageName = "Vector Spear";
                break;
            case "SwordBladeDancerCelestial":
                imageName = weaponSkinName = "Moonbeam Blade (Chakora Priya)";
                break;
            case "FistsSantaShang":
                imageName = weaponSkinName = "Holly Jolly (Santa Wu Shang)";
                break;
            case "AxeHolidayXull":
                displayName = imageName = weaponSkinName = "World Cleaver (Abominable Jötunn Xull)";
                break;
        }

        if (WeaponSkinTypes.UpgradeLevel.TryGetValue(weaponSkin, out int upgradeLevel) && upgradeLevel != 0)
        {
            displayName = weaponSkinName + " (Lvl " + upgradeLevel + ")";
            imageName = weaponSkinName + " Level " + upgradeLevel;
        }

        weaponSkinName = weaponSkinName.Replace(":", "&#58;");
        displayName = displayName.Replace(":", "&#58;");
        imageName = imageName.Replace(":", "");

        return (weaponSkinName, imageName, displayName);
    }
    public (string companionName, string imageName, string displayName) GetCompanionNameParams(CompanionType companionType)
    {
        // string companion = companionType.CompanionName;
        string displayNameKey = companionType.DisplayNameKey;

        string companionName = LangFile.Entries[displayNameKey];
        string imageName = companionName;
        string displayName = companionName;

        companionName = companionName.Replace(":", "&#58;");
        displayName = displayName.Replace(":", "&#58;");
        imageName = imageName.Replace(":", "");

        return (companionName, imageName, displayName);
    }

    public CostumeType? GetWeaponSkinSourceCostume(WeaponSkinType weaponSkin)
    {
        InitWeaponSkinToCostume();
        return WeaponSkinToCostume.GetValueOrDefault(weaponSkin.WeaponSkinName);
    }
}