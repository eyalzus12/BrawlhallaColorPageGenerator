using System.IO;
using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaLangReader;

namespace BrawlhallaColorPageGenerator.Writers;

public sealed class WeaponSkinsWriter(WeaponSkinTypes weaponSkinTypes, LangFile langFile)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine("<includeonly><onlyinclude>");
        writer.WriteLine("The following is a list of all weapon skins in {{{1|}}}. ''Click an image to view it in higher resolution.''");
        writer.WriteLine();

        string? currentBaseWeapon = null;
        foreach (WeaponSkinType weaponSkin in weaponSkinTypes.WeaponSkins)
        {
            string baseWeapon = weaponSkin.BaseWeapon;
            if (currentBaseWeapon != baseWeapon)
            {
                if (currentBaseWeapon is not null)
                {
                    writer.WriteLine("}}");
                    writer.WriteLine();
                }
                writer.Write("===[[");
                writer.Write(Utils.BASE_WEAPON_NAME[baseWeapon]);
                writer.WriteLine("]]===");
                writer.WriteLine("{{List to itembox|color={{{1|}}}|");
            }
            currentBaseWeapon = baseWeapon;

            if (
                weaponSkin.WeaponSkinName == "Template" ||
                weaponSkin.DisplayNameKey is null ||
                !weaponSkin.CanColorSwap ||
                weaponSkin.WeaponSkinName.EndsWith("Stub") ||
                weaponSkin.WeaponSkinName.EndsWith("EivorMale") ||
                weaponSkin.WeaponSkinName.EndsWith("Stance")
            ) continue;

            (string weaponSkinName, string imageName, string displayName) = GetNameParams(weaponSkin);

            writer.Write(weaponSkinName);
            if (weaponSkinName != displayName)
            {
                writer.Write(" && displayname:");
                writer.Write(displayName);
            }
            if (weaponSkinName != imageName)
            {
                writer.Write(" && image:");
                writer.Write(imageName);
                writer.Write(" $1.png");
            }
            writer.WriteLine();
        }
        writer.WriteLine("}}");
        writer.WriteLine();

        writer.WriteLine("[[Category:Weapon Skins in all colors]]</onlyinclude></includeonly>");
        writer.WriteLine("<noinclude>");
        writer.WriteLine("{{doc}}");
        writer.WriteLine("[[Category:Templates]]");
        writer.WriteLine("</noinclude>");
    }

    private (string weaponSkinName, string imageName, string displayName) GetNameParams(WeaponSkinType weaponSkinType)
    {
        string weaponSkin = weaponSkinType.WeaponSkinName;
        string displayNameKey = weaponSkinType.DisplayNameKey!;

        string weaponSkinName = langFile.Entries[displayNameKey];
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

        if (weaponSkinTypes.UpgradeLevel.TryGetValue(weaponSkin, out int upgradeLevel) && upgradeLevel != 0)
        {
            displayName = weaponSkinName + " (Lvl " + upgradeLevel + ")";
            imageName = weaponSkinName + " Level " + upgradeLevel;
        }

        weaponSkinName = weaponSkinName.Replace(":", "&#58;");
        displayName = displayName.Replace(":", "&#58;");
        imageName = imageName.Replace(":", "");

        return (weaponSkinName, imageName, displayName);
    }
}