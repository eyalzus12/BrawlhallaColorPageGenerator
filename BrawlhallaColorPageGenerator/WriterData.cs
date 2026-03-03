using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaLangReader;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public required CostumeTypes CostumeTypes { get; init; }
    public required WeaponSkinTypes WeaponSkinTypes { get; init; }
    public required HeroTypes HeroTypes { get; init; }
    public required RuneTypes RuneTypes { get; init; }
    public required CompanionTypes CompanionTypes { get; init; }
    public required StoreTypes StoreTypes { get; init; }
    public required EntitlementTypes EntitlementTypes { get; init; }
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

    public (string weaponSkinName, string imageName, string displayName) GetWeaponSkinNameParams(WeaponSkinType weaponSkinType, bool colorMode)
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
                if (!colorMode) imageName = "Gilded Glory (Axe)";
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
            case "AxeJotun":
                weaponSkinName = displayName = "World Cleaver (Jotun Ulgrim)";
                break;
            case "AxeHolidayXull":
                displayName = imageName = weaponSkinName = "World Cleaver (Abominable Jötunn Xull)";
                break;
        }

        if (colorMode && WeaponSkinTypes.UpgradeLevel.TryGetValue(weaponSkin, out int upgradeLevel) && upgradeLevel != 0)
        {
            displayName = weaponSkinName + " (Lvl " + upgradeLevel + ")";
            imageName = weaponSkinName + " Level " + upgradeLevel;
        }

        if (colorMode)
        {
            weaponSkinName = weaponSkinName.Replace(":", "&#58;");
            displayName = displayName.Replace(":", "&#58;");
        }
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

    private static readonly HashSet<string> _vsrWeaponSkins = [
        // charity
        "Acuity Manifest",
        "Skysworn Oath",
        "Balanced Counsel",
        "Stalwart Screech",
        "Strike of the Wise",
        "Wings of the Sage",
        "The Giver's Grasp",
        "Perceptive Flight",
        "Erudition's Call",
        "Spear of Wisdom",
        // mahou shoujo
        "Striking Magi ☆ Miracle Mallet",
        "Fae Magi ☆ Soaring Locket",
        "Soaring Magi ☆ Sky Duster",
        // neo city
        "Neo City Cannon",
        "Neo City Hammer",
        "Neo-City Greatsword",
        "Neo City Spear",
        "Neo-City Sword",
    ];

    private static readonly HashSet<string> _unobtainableWeaponSkins = [
        "Gear Death Experience",
        "Volcanic Edge",
        "Blaster Pistols",
        "Golden Gears",
        "Venom Spitters",
        "Mk1 Cannon",
        "Combustion",
        "Ix Chel",
        "Scrapyard Bruiser",
        "The Kingsman",
        "Titanium Crusher",
        "Torii Mallet",
        "Edge Guards",
        "MK Needles",
        "Neon Surfer",
        "Sprocketeer",
        "Golden Saber",
        "MK Katana",
        "Obsidian Edge",
        "Outlander's Knife",
        "Power Sword",
        "Salvaged Razor",
    ];

    private static readonly HashSet<string> _giftsOfAsgard = [
        "Axe of the World Eagle",
        "Hraesvelgr's Eyes",
        "Tyr's Fists",
        "Katars of the Raven",
        "Raven's Talon",
        "Odin's Spear",
        "Sword of the Raven",
    ];

    public string GetWeaponSkinMiscDesc(WeaponSkinType weaponSkin)
    {
        string displayNameKey = weaponSkin.DisplayNameKey!;
        string weaponSkinName = LangFile.Entries[displayNameKey];

        if (_vsrWeaponSkins.Contains(weaponSkinName))
            return "[[Brawlhalla Viewership Rewards|Viewership Rewards]]";
        if (_unobtainableWeaponSkins.Contains(weaponSkinName))
            return "''Unobtainable''";
        if (_giftsOfAsgard.Contains(weaponSkinName))
            return "[[The Gifts of Asgard]]";

        return weaponSkinName switch
        {
            // bp 1
            "Demonic Slab" => "{{BPReward|Season One|gold|Tier 29}}",
            "Fiendish Howl" => "{{BPReward|Season One|gold|Tier 50}}",
            "Cursed Bow" => "{{BPReward|Season One|gold|Tier 57}}",
            "Dragon's Fire" => "{{BPReward|Season One|free|Tier 6}}",
            "Eye of the Oni" => "{{BPReward|Season One|gold|Tier 65}}",
            "Kanabo Korosu" => "{{BPReward|Season One Classic|gold|Tier 81}}",
            "Yokai Slam" => "{{BPReward|Season One|gold|Tier 41}}",
            "Dark Scarabs" => "{{BPReward|Season One|gold|Tier 9}}",
            "Sleeping Demon" => "{{BPReward|Season One|gold|Tier 25}}",
            "Yokai's Spirit" => "{{BPReward|Season One|gold|Tier 19}}",
            "Jaws of the Oni" => "{{BPReward|Season One|gold|Tier 79}}",
            "Devilish Spike" => "{{BPReward|Season One|gold|Tier 37}}",
            "Devil's Hand" => "{{BPReward|Season One|gold|Tier 15}}",

            // bp 2
            "Sunset Axe" => "{{BPReward|Season Two|gold|Tier 15}}",
            "Time is Running (Out)" => "{{BPReward|Season Two Classic|free|Tier 62}}",
            "Tempo and Groove" => "{{BPReward|Season Two|gold|Tier 41}}",
            "Smooth Waves" => "{{BPReward|Season Two|free|Tier 6}}",
            "Sonic Boom" => "{{BPReward|Season Two|gold|Tier 57}}",
            "Nightlapse" => "{{BPReward|Season Two|gold|Tier 50}}",
            "Outrun" => "{{BPReward|Season Two|gold|Tier 81}}",
            "Offworld" => "{{BPReward|Season Two|gold|Tier 9}}",
            "kaTR-808" => "{{BPReward|Season Two|gold|Tier 25}}",
            "Galactic Glitch" => "{{BPReward|Season Two|gold|Tier 65}}",
            "Synthetic Charge" => "{{BPReward|Season Two|gold|Tier 29}}",
            "Digital Lockdown" => "{{BPReward|Season Two|gold|Tier 37}}",
            "Astro Shard" => "{{BPReward|Season Two|gold|Tier 79}}",
            "Neon Gleam" => "{{BPReward|Season Two|gold|Tier 19}}",

            // bp 3
            "Vetr Bearded Axe" => "{{BPReward|Season Three|gold|Tier 37}}",
            "Beowulf Crushers" => "{{BPReward|Season Three Classic|gold|Tier 69}}",
            "Jötunn Armaments" => "{{BPReward|Season Three|gold|Tier 53}}",
            "Skadi's Bow" => "{{BPReward|Season Three|gold|Tier 25}}",
            "Snowsmoke" => "{{BPReward|Season Three|gold|Tier 41}}",
            "Crystal Clutch" => "{{BPReward|Season Three|gold|Tier 69|small}}<br>{{BPReward|Season Three Classic|free|Tier 66|small}}",
            "Glacier Strike" => "{{BPReward|Season Three|gold|Tier 57}}",
            "Ymir's Sledge" => "{{BPReward|Season Three|gold|Tier 77}}",
            "Black Icicles" => "{{BPReward|Season Three|free|Tier 6}}",
            "Essence of Niflheim" => "{{BPReward|Season Three|gold|Tier 9}}",
            "Drivsnö Pulse" => "{{BPReward|Season Three|gold|Tier 21}}",
            "Wrath of Hel" => "{{BPReward|Season Three|gold|Tier 81}}",
            "Tundra Geir" => "{{BPReward|Season Three|gold|Tier 15}}",
            "Frost Brandr" => "{{BPReward|Season Three|gold|Tier 29}}",

            // bp 4
            "Royal Crescent" => "{{BPReward|Season Four|gold|Tier 21}}",
            "Fang & Claw" => "{{BPReward|Season Four Classic|gold|Tier 69}}",
            "Flintlock Claws" => "{{BPReward|Season Four|gold|Tier 25}}",
            "Prideful Roar" => "{{BPReward|Season Four|gold|Tier 29}}",
            "Booming Belfry" => "{{BPReward|Season Four|gold|Tier 9}}",
            "Lion's Reign" => "{{BPReward|Season Four|free|Tier 6}}",
            "Glory of the Lions" => "{{BPReward|Season Four|gold|Tier 69}}<br>{{BPReward|Season Four Classic|free|Tier 66}}",
            "Mosaic Maul" => "{{BPReward|Season Four|gold|Tier 15}}",
            "Stained Shards" => "{{BPReward|Season Four|gold|Tier 57}}",
            "Lighted Eminence" => "{{BPReward|Season Four|gold|Tier 41}}",
            "Exalting Spire" => "{{BPReward|Season Four|gold|Tier 37}}",
            "Arched Valor" => "{{BPReward|Season Four|gold|Tier 53}}",
            "Righteous Spine" => "{{BPReward|Season Four|gold|Tier 81}}",
            "Saber of Order" => "{{BPReward|Season Four|gold|Tier 77}}",

            // bp 5
            "Jupiter Rising" => "{{BPReward|Season Five|gold|Tier 77}}",
            "Stellar Strikes" => "{{BPReward|Season Five Classic|gold|Tier 69}}",
            "Gemini's Wrath" => "{{BPReward|Season Five|free|Tier 6}}",
            "Sagittarius Crescent" => "{{BPReward|Season Five|gold|Tier 53}}",
            "The Big Bang" => "{{BPReward|Season Five|gold|Tier 41}}",
            "Infinite & Absolute" => "{{BPReward|Season Five|gold|Tier 25}}",
            "Twilight Cleaver" => "{{BPReward|Season Five|gold|Tier 21}}",
            "Galactic Gavel" => "{{BPReward|Season Five|gold|Tier 81}}",
            "Zenith Daggers" => "{{BPReward|Season Five|gold|Tier 9}}",
            "Photon Sphere" => "{{BPReward|Season Five|gold|Tier 57}}",
            "Retrograde Rocket" => "{{BPReward|Season Five|gold|Tier 29}}",
            "Singularity Sickle" => "{{BPReward|Season Five|gold|Tier 37}}",
            "Twin Solstice" => "{{BPReward|Season Five|gold|Tier 69}}<br>{{BPReward|Season Five Classic|free|Tier 65}}",
            "Astroblade" => "{{BPReward|Season Five|gold|Tier 15}}",

            // bp 6
            "Beckoning Bramble" => "{{BPReward|Season Six|gold|Tier 37}}",
            "Beetle Stampede" => "{{BPReward|Season Six Classic|gold|Tier 35}}",
            "Grisly Burrs" => "{{BPReward|Season Six|gold|Tier 9}}",
            "Thistle's Flight" => "{{BPReward|Season Six|gold|Tier 29}}",
            "Blistering Bellows" => "{{BPReward|Season Six|gold|Tier 57}}",
            "Wheel of Thorns" => "{{BPReward|Season Six Classic|gold|Tier 73}}",
            "Spine-Chilling Fists" => "{{BPReward|Season Six|free|Tier 6}}",
            "Flamberge's Gale" => "{{BPReward|Season Six|gold|Tier 77}}",
            "Wild's Smasher" => "{{BPReward|Season Six|gold|Tier 69}}",
            "Briar Barbs" => "{{BPReward|Season Six|gold|Tier 25}}",
            "Eldritch Core" => "{{BPReward|Season Six|gold|Tier 41}}",
            "Pulsing Thicket" => "{{BPReward|Season Six|gold|Tier 15}}",
            "Withering Scythe" => "{{BPReward|Season Six|gold|Tier 53}}",
            "Pike of the Forgotten" => "{{BPReward|Season Six|gold|Tier 81}}",
            "Prickly Cut" => "{{BPReward|Season Six|gold|Tier 21}}",

            // bp 7
            "Dwarven-Forged Axe" => "{{BPReward|Season Seven|gold|Tier 53}}",
            "Dwarven-Forged Boots" => "{{BPReward|Season Seven|gold|Tier 9}}",
            "Dwarven-Forged Blasters" => "{{BPReward|Season Seven|gold|Tier 17}}",
            "Dwarven-Forged Bow" => "{{BPReward|Season Seven|gold|Tier 37}}",
            "Dwarven-Forged Cannon" => "{{BPReward|Season Seven|gold|Tier 15}}",
            "Dwarven-forged Chakram" => "{{BPReward|Season Seven Classic|gold|Tier 21}}",
            "Dwarven-Forged Gauntlets" => "{{BPReward|Season Seven|gold|Tier 57}}",
            "Dwarven-Forged Greatsword" => "{{BPReward|Season Seven|gold|Tier 21}}<br>{{BPReward|Season Seven Classic|free|Tier 20}}",
            "Dwarven-Forged Hammer" => "{{BPReward|Season Seven|free|Tier 6}}",
            "Dwarven-Forged Katars" => "{{BPReward|Season Seven|gold|Tier 41}}",
            "Dwarven-Forged Orb" => "{{BPReward|Season Seven|gold|Tier 25}}",
            "Dwarven-Forged Lance" => "{{BPReward|Season Seven|gold|Tier 29}}",
            "Dwarven-Forged Scythe" => "{{BPReward|Season Seven|gold|Tier 69}}",
            "Dwarven-Forged Spear" => "{{BPReward|Season Seven|gold|Tier 77}}",
            "Dwarven-Forged Sword" => "{{BPReward|Season Seven|gold|Tier 81}}",

            // bp 8
            "Cyber Myk Axe" => "{{BPReward|Season Eight|gold|Tier 13}}",
            "Cyber Myk Megaboots" => "{{BPReward|Season Eight|gold|Tier 63}}",
            "Cyber Myk Pistols" => "{{BPReward|Season Eight|gold|Tier 37}}",
            "Cyber Myk Bow" => "{{BPReward|Season Eight|gold|Tier 26}}",
            "Cyber Myk Megacannon" => "{{BPReward|Season Eight|gold|Tier 18}}",
            "Cyber Myk Gauntlets" => "{{BPReward|Season Eight|gold|Tier 33}}",
            "Cyber Myk Claymore" => "{{BPReward|Season Eight|gold|Tier 58}}",
            "Cyber Myk Gavel" => "{{BPReward|Season Eight|gold|Tier 67}}",
            "Cyber Myk Shanks" => "{{BPReward|Season Eight|free|Tier 78}}",
            "Cyber Myk Orb" => "{{BPReward|Season Eight|free|Tier 4}}",
            "Cyber Myk Sickle" => "{{BPReward|Season Eight|gold|Tier 83}}",
            "Cyber Myk Switchblade" => "{{BPReward|Season Eight|gold|Tier 73}}",

            // bp 9
            "Axe of Mercy" => "{{BPReward|Season Nine|gold|Tier 18}}",
            "Boots of Mercy" => "{{BPReward|Season Nine|gold|Tier 63}}",
            "Blasters of Mercy" => "{{BPReward|Season Nine|gold|Tier 33}}",
            "Cannon of Mercy" => "{{BPReward|Season Nine|gold|Tier 73}}",
            "Gauntlets of Mercy" => "{{BPReward|Season Nine|free|Tier 78}}",
            "Greatsword of Mercy" => "{{BPReward|Season Nine|gold|Tier 83}}",
            "Hammer of Mercy" => "{{BPReward|Season Nine|free|Tier 4}}",
            "Katars of Mercy" => "{{BPReward|Season Nine|gold|Tier 67}}",
            "Orb of Mercy" => "{{BPReward|Season Nine|gold|Tier 37}}",
            "Rocket Lance of Mercy" => "{{BPReward|Season Nine|gold|Tier 26}}",
            "Spear of Mercy" => "{{BPReward|Season Nine|gold|Tier 58}}",
            "Sword of Mercy" => "{{BPReward|Season Nine|gold|Tier 13}}",

            // bp 10
            "Valiant Armament Axe" => "{{BPReward|Season Ten|gold|Tier 26}}",
            "Valiant Armament Boots" => "{{BPReward|Season Ten|gold|Tier 33}}",
            "Valiant Armament Blasters" => "{{BPReward|Season Ten|gold|Tier 37}}",
            "Valiant Armament Bow" => "{{BPReward|Season Ten|gold|Tier 58}}",
            "Valiant Armament Cannon" => "{{BPReward|Season Ten|gold|Tier 13}}",
            "Valiant Armament Gauntlets" => "{{BPReward|Season Ten|free|Tier 78}}",
            "Valiant Armament Katars" => "{{BPReward|Season Ten|free|Tier 4}}",
            "Valiant Armament Orb" => "{{BPReward|Season Ten|gold|Tier 63}}",
            "Valiant Armament Rocket Lance" => "{{BPReward|Season Ten|gold|Tier 18}}",
            "Valiant Armament Scythe" => "{{BPReward|Season Ten|gold|Tier 67}}",
            "Valiant Armament Spear" => "{{BPReward|Season Ten|gold|Tier 73}}",
            "Valiant Armament Sword" => "{{BPReward|Season Ten|gold|Tier 83}}",

            // bp 11
            "Aztlán Axe" => "{{BPReward|Season Eleven|gold|Tier 73}}",
            "Aztlán Boots" => "{{BPReward|Season Eleven|gold|Tier 58}}",
            "Aztlán Blasters" => "{{BPReward|Season Eleven|free|Tier 4}}",
            "Aztlán Bow" => "{{BPReward|Season Eleven|gold|Tier 13}}",
            "Aztlán Cannon" => "{{BPReward|Season Eleven|gold|Tier 26}}",
            "Aztlán Chakram" => "{{BPReward|Season Eleven|gold|Tier 73}}",
            "Aztlán Gauntlets" => "{{BPReward|Season Eleven|gold|Tier 45}}",
            "Aztlán Greatsword" => "{{BPReward|Season Eleven|gold|Tier 18}}",
            "Aztlán Hammer" => "{{BPReward|Season Eleven|gold|Tier 63}}",
            "Aztlán Orb" => "{{BPReward|Season Eleven|gold|Tier 37}}",
            "Aztlán Rocket Lance" => "{{BPReward|Season Eleven|gold|Tier 33}}",
            "Aztlán Scythe" => "{{BPReward|Season Eleven|gold|Tier 83}}",
            "Aztlán Spear" => "{{BPReward|Season Eleven|gold|Tier 67}}",

            // bp 12
            "Ninpō: Unsealed Axe" => "{{BPReward|Season Twelve|gold|Present Tier 6}}",
            "Ninpō: Unsealed Boots" => "{{BPReward|Season Twelve|gold|Past Tier 14}}",
            "Ninpō: Unsealed Blasters" => "{{BPReward|Season Twelve|free|Present Tier 4}}",
            "Ninpō: Unsealed Bow" => "{{BPReward|Season Twelve|free|Past Tier 23}}",
            "Ninpō: Unsealed Cannon" => "{{BPReward|Season Twelve|free|Future Tier 1}}",
            "Ninpō: Unsealed Chakram" => "{{BPReward|Season Twelve|gold|Past Tier 22}}",
            "Ninpō: Unsealed Greatsword" => "{{BPReward|Season Twelve|free|Present Tier 23}}",
            "Ninpō: Unsealed Hammer" => "{{BPReward|Season Twelve|gold|Past Tier 8}}",
            "Ninpō: Unsealed Katars" => "{{BPReward|Season Twelve|free|Intro Tier 5}}",
            "Ninpō: Unsealed Rocket Lance" => "{{BPReward|Season Twelve|free|Past Tier 4}}",
            "Ninpō: Unsealed Scythe" => "{{BPReward|Season Twelve|gold|Future Tier 21}}",
            "Ninpō: Unsealed Spear" => "{{BPReward|Season Twelve|gold|Future Tier 4}}",
            "Ninpō: Unsealed Sword" => "{{BPReward|Season Twelve|free|Future Tier 23}}",

            // others
            "Apex Keysword" => "SteelSeries 2022 reward",
            "Logitech Keycap" => "Tournament Reward",

            _ => "ERROR",
        };
    }

    private static readonly HashSet<string> _longWeaponSkinNameSet = [
        "Brave Magi ☆ Twinkling Justice",
        "Chocolate Cherry Sunburst",
        "World Cleaver",
    ];

    public bool GetWeaponSkinNameIsLong(WeaponSkinType weaponSkin)
    {
        string displayNameKey = weaponSkin.DisplayNameKey!;
        string weaponSkinName = LangFile.Entries[displayNameKey];
        return _longWeaponSkinNameSet.Contains(weaponSkinName);
    }
}