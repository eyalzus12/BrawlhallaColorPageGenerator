using System.IO;
using System.Linq;
using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaLangReader;

namespace BrawlhallaColorPageGenerator.Writers;

public sealed class WeaponSkinsWriter(WeaponSkinTypes weaponSkinTypes, LangFile langFile)
{
    public void WriteTo(string path)
    {
        foreach ((string baseWeapon, string weaponName) in Utils.BASE_WEAPON_NAME)
        {
            using StreamWriter weaponWriter = new(path.Replace("\x00", weaponName));
            weaponWriter.WriteLine("{{itembox/top}}");
            foreach (WeaponSkinType weaponSkin in weaponSkinTypes.WeaponSkins)
            {
                if (
                    weaponSkin.WeaponSkinName == "Template" ||
                    weaponSkin.BaseWeapon != baseWeapon ||
                    weaponSkin.DisplayNameKey is null ||
                    !weaponSkin.CanColorSwap ||
                    weaponSkin.WeaponSkinName.EndsWith("Stub") ||
                    weaponSkin.WeaponSkinName.EndsWith("EivorMale") ||
                    weaponSkin.WeaponSkinName.EndsWith("Stance")
                ) continue;

                (string weaponSkinName, string imageName, string displayName) = GetNameParams(weaponSkin);

                weaponWriter.Write("{{Color Weapon Skins/Single|color={{{1|}}}|width={{{width|}}}|height={{{height|}}}|name=");
                weaponWriter.Write(weaponSkinName);
                if (weaponSkinName != displayName)
                {
                    weaponWriter.Write("|displayname=");
                    weaponWriter.Write(displayName);
                }
                weaponWriter.Write("|image=");
                weaponWriter.Write(imageName);
                weaponWriter.WriteLine("}}");
            }
            weaponWriter.WriteLine("{{itembox/bottom}}");
        }

        // write main page
        using StreamWriter mainWriter = new(path.Replace("\x00", "main"));
        mainWriter.WriteLine("<includeonly><onlyinclude>");
        mainWriter.WriteLine("The following is a list of all weapon skins in {{{1|}}}. ''Click an image to view it in higher resolution.''");
        mainWriter.WriteLine();

        foreach ((_, string weaponName) in Utils.BASE_WEAPON_NAME.OrderBy((e) => e.Value))
        {
            mainWriter.Write("===[[");
            mainWriter.Write(weaponName);
            mainWriter.WriteLine("]]===");

            mainWriter.Write("{{Color Weapon Skins/");
            mainWriter.Write(weaponName);
            mainWriter.WriteLine("|{{{1|}}}}}");

            mainWriter.WriteLine();
        }

        mainWriter.WriteLine("[[Category:Weapon Skins in all colors]]</onlyinclude></includeonly>");
        mainWriter.WriteLine("<noinclude>");
        mainWriter.WriteLine("{{doc}}");
        mainWriter.WriteLine("[[Category:Templates]]");
        mainWriter.WriteLine("</noinclude>");
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

        imageName = imageName.Replace(":", "");

        return (weaponSkinName, imageName, displayName);
    }
}