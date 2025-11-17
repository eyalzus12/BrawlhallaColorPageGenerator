using System.IO;
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
        string currentBaseWeapon = "";
        foreach (WeaponSkinType weaponSkin in weaponSkinTypes.WeaponSkins)
        {
            if (
                weaponSkin.WeaponSkinName == "Template" ||
                weaponSkin.DisplayNameKey is null ||
                !weaponSkin.CanColorSwap ||
                weaponSkin.WeaponSkinName.EndsWith("Stub") ||
                weaponSkin.WeaponSkinName.EndsWith("EivorMale") ||
                weaponSkin.WeaponSkinName.EndsWith("Stance")
            ) continue;

            if (weaponSkin.BaseWeapon != currentBaseWeapon)
            {
                if (!string.IsNullOrEmpty(currentBaseWeapon))
                {
                    writer.WriteLine("{{itembox/bottom}}");
                    writer.WriteLine();
                }

                writer.WriteLine("===[[¹]]===".Apply(Utils.BASE_WEAPON_NAME[weaponSkin.BaseWeapon]));
                writer.WriteLine("{{itembox/top}}");
                currentBaseWeapon = weaponSkin.BaseWeapon;
            }

            (string weaponSkinName, string imageName, string displayName) = GetNameParams(weaponSkin);

            writer.WriteLine("{{itembox|width=150|height=150|name=¹²|image=³ {{{1|}}}.png|compact=true|noimglink=true}}".Apply3(
                weaponSkinName,
                weaponSkinName == displayName ? "" : ("|displayname=" + displayName),
                imageName
            ));
        }
        writer.WriteLine("{{itembox/bottom}}");

        writer.WriteLine("[[Category:Weapon Skins in all colors]]</onlyinclude></includeonly>");
        writer.WriteLine("<noinclude>");
        writer.WriteLine("{{doc}}");
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