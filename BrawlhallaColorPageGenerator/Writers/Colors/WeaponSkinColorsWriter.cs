using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers.Colors;

public sealed class WeaponSkinColorsWriter(WriterData data)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine("<includeonly><onlyinclude>");
        writer.WriteLine("The following is a list of all weapon skins in {{{1|}}}. ''Click an image to view it in higher resolution.''");
        writer.WriteLine();

        string? currentBaseWeapon = null;
        foreach (WeaponSkinType weaponSkin in data.WeaponSkinTypes.WeaponSkins)
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

            (string weaponSkinName, string imageName, string displayName) = data.GetWeaponSkinNameParams(weaponSkin);

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
}