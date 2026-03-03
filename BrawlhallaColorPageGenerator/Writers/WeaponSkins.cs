using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers.Colors;

public sealed class WeaponSkinWriter(WriterData data)
{
    public void WriteTo(string path, string baseWeapon)
    {
        using StreamWriter writer = new(path);
        writer.Write("The following is a list of all ");
        writer.Write(Utils.BASE_WEAPON_NAME[baseWeapon]);
        writer.WriteLine("]] skins in [[Brawlhalla]]");
        writer.WriteLine();

        writer.Write("==");
        writer.Write(Utils.BASE_WEAPON_NAME[baseWeapon]);
        writer.WriteLine(" Skins==");

        writer.WriteLine("{{itembox/top}}");
        foreach (WeaponSkinType weaponSkin in data.WeaponSkinTypes.WeaponSkins)
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

            (string weaponSkinName, string imageName, string displayName) = data.GetWeaponSkinNameParams(weaponSkin);
            writer.Write("{{itembox|width=205|height=170|name=");
            writer.Write(weaponSkinName);
            if (weaponSkinName != displayName)
            {
                writer.Write("|displayname=");
                writer.Write(displayName);
            }
            writer.Write("|image=");
            writer.Write(imageName);
            writer.Write(".png");
            // figure out description
            if (data.GetWeaponSkinSourceCostume(weaponSkin) is CostumeType costume)
            {
                writer.Write("|desc=[[");
                (string costumeName, _, _) = data.GetSkinNameParams(costume);
                writer.Write(costumeName);
                writer.Write("]]");
            }


            writer.WriteLine("|costheight=55px}}");
        }
        writer.WriteLine("{{itembox/bottom}}");
    }
}