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

            string weaponSkinName = langFile.Entries[weaponSkin.DisplayNameKey];
            string imageName = weaponSkinName;
            string displayName = "";

            if (weaponSkin.WeaponSkinName == "AxeSimon")
            {
                weaponSkinName = "Battle Axe (Simon Belmont)";
                imageName = "Battle Axe (Simon Belmont)";
            }
            else if (weaponSkin.WeaponSkinName == "AxeGilded")
            {
                weaponSkinName = "Gilded Glory (Axe Skin)";
                displayName = "Gilded Glory";
            }
            else if (weaponSkin.WeaponSkinName == "AxeActualValk")
            {
                weaponSkinName = "Glory (Weapon Skin)";
                displayName = "Glory";
            }
            else if (weaponSkin.WeaponSkinName == "PistolSerape")
            {
                weaponSkinName = "Snake Eyes (Weapon Skin)";
                imageName = "Snake Eyes (Weapon Skin)";
                displayName = "Snake Eyes";
            }
            else if (weaponSkin.WeaponSkinName == "BowOldKoji")
            {
                weaponSkinName = "Heirloom (Bow Skin)";
                displayName = "Heirloom";
            }
            else if (weaponSkin.WeaponSkinName == "CannonDestinyTitan")
            {
                imageName = "Dragon's Breath (Titan)";
            }
            else if (weaponSkin.WeaponSkinName == "FistsVolcano")
            {
                weaponSkinName = "Hot Lava (Weapon Skin)";
                displayName = "Hot Lava";
            }
            else if (weaponSkin.WeaponSkinName == "FistsOrb4")
            {
                weaponSkinName = "Knockouts (Weapon Skin)";
                displayName = "Knockouts";
            }
            else if (weaponSkin.WeaponSkinName == "HammerMadame")
            {
                weaponSkinName = "Heirloom (Hammer Skin)";
                imageName = "Heirloom (Hammer Skin)";
                displayName = "Heirloom";
            }
            else if (weaponSkin.WeaponSkinName == "RocketLanceMotorcycle")
            {
                weaponSkinName = "Burnout (Weapon Skin)";
                displayName = "Burnout";
            }
            else if (weaponSkin.WeaponSkinName == "SpearGem")
            {
                weaponSkinName = "Dusk (Weapon Skin)";
                imageName = "Dusk (Weapon Skin)";
                displayName = "Dusk";
            }
            else if (weaponSkin.WeaponSkinName == "SpearViral")
            {
                weaponSkinName = "Vector (Weapon Skin)";
                imageName = "Vector Spear";
                displayName = "Vector";
            }
            else if (weaponSkin.WeaponSkinName == "SwordBladeDancerCelestial")
            {
                weaponSkinName = "Moonbeam Blade (Chakora Priya)";
                imageName = "Moonbeam Blade (Chakora Priya)";
                displayName = "Moonbeam Blade";
            }

            if (weaponSkinTypes.UpgradeLevel.TryGetValue(weaponSkin.WeaponSkinName, out int upgradeLevel) && upgradeLevel != 0)
            {
                displayName = weaponSkinName + " (Lvl " + upgradeLevel + ")";
                imageName = weaponSkinName + " Level " + upgradeLevel;
            }

            imageName = imageName.Replace(":", "");

            writer.WriteLine("{{itembox|width=150|height=150|name=¹²|image=³ {{{1|}}}.png|compact=true|noimglink=true}}".Apply3(
                weaponSkinName,
                string.IsNullOrEmpty(displayName) ? "" : ("|displayname=" + displayName),
                imageName
            ));
        }
        writer.WriteLine("{{itembox/bottom}}");

        writer.WriteLine("[[Category:Weapon Skins in all colors]]</onlyinclude></includeonly>");
        writer.WriteLine("<noinclude>");
        writer.WriteLine("{{doc}}");
        writer.WriteLine("</noinclude>");
    }
}