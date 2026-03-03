using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
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
}