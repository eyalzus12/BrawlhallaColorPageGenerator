using System.Collections.Generic;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    private static readonly HashSet<string> _longWeaponSkins = [
        "AxeMagicalGirl",
        "AxeSpringAxe21Viewer",
        "AxeHolidayXull",
        "AxeJotun",
        "BootsMagicalGirl",
        "PistolHighwayman",
        "PistolChewbacca",
        "PistolHanSolo",
        "PistolEmperor",
        "PistolBP7",
        "PistolLilith",
        "PistolMando",
        "PistolPearlDerringer",
        "PistolKayVess",
        "PistolBP10Mecha",
        "PistolBP12",
        "CannonBP7",
        "CannonRoboPuppet",
        "CannonBP12",
        "CannonBP10Mecha",
        "FistsBP7",
        "FistsPetraBP12",
        "FistsObiWan",
        "FistsBP10Mecha",
        "GreatswordAsgardSaber",
        "GreatswordBP7",
        "GreatswordBP12",
        "GreatswordMechaArmor",
        "HammerMagicalGirl",
        "KatarAhsoka",
        "KatarAsgardSaber"
    ];

    public (string weaponSkinName, string imageName, string displayName, bool isAnimated) GetWeaponSkinNameParams(WeaponSkinType weaponSkinType, bool colorMode)
    {
        string weaponSkin = weaponSkinType.WeaponSkinName;
        string displayNameKey = weaponSkinType.DisplayNameKey!;

        bool isAnimated = false;
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
                if (colorMode) imageName = weaponSkinName;
                break;
            case "BowOldKoji":
                weaponSkinName = "Heirloom (Bow Skin)";
                if (!colorMode) imageName = "Heirloom (Bow)";
                break;
            case "CannonDestinyTitan":
                imageName = "Dragon's Breath (Titan)";
                break;
            case "FistsVolcano":
                weaponSkinName = "Hot Lava (Weapon Skin)";
                break;
            case "FistsOrb4":
                weaponSkinName = "Knockouts (Weapon Skin)";
                if (!colorMode) imageName = "Knockouts (Weapon Skin)";
                break;
            case "HammerMadame":
                weaponSkinName = "Heirloom (Hammer Skin)";
                imageName = colorMode ? weaponSkinName : "Heirloom (Hammer)";
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
            case "PistolBubblegum":
                isAnimated = true;
                if (!colorMode) imageName = "AniElectrode Guns";
                break;
            case "PistolMythicNix":
                isAnimated = true;
                break;
            case "BowCyberSam":
                isAnimated = true;
                if (!colorMode) imageName = "AniDaemon Killer";
                break;
            case "BowTrickOrTreat":
                if (!colorMode) imageName = "ded";
                break;
            case "FistsDemon01":
            case "FistsDemon02":
            case "FistsDemon03":
                isAnimated = true;
                if (!colorMode) imageName = "AniHaunting Terrors";
                break;
            case "FistsMythicWuShang":
                isAnimated = true;
                break;
            case "HammerScientist":
                isAnimated = true;
                if (!colorMode) imageName = "AniCircuit Breaker";
                break;
        }

        // Poppin’ TNTina
        weaponSkinName = weaponSkinName.Replace('’', '\'');
        displayName = displayName.Replace('’', '\'');
        imageName = imageName.Replace('’', '\'');

        // progression level
        if (colorMode && WeaponSkinTypes.UpgradeLevel.TryGetValue(weaponSkin, out int upgradeLevel) && upgradeLevel != 0)
        {
            displayName = weaponSkinName + " (Lvl " + upgradeLevel + ")";
            imageName = weaponSkinName + " Level " + upgradeLevel;
        }

        // names that are too long
        if (!colorMode && _longWeaponSkins.Contains(weaponSkin))
        {
            displayName = "{{small|" + displayName + "}}";
        }

        // html escape for the template
        if (colorMode)
        {
            weaponSkinName = weaponSkinName.Replace(":", "&#58;");
            displayName = displayName.Replace(":", "&#58;");
        }

        // no : in image names
        imageName = imageName.Replace(":", "");

        return (weaponSkinName, imageName, displayName, isAnimated);
    }
}