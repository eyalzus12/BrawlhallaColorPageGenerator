using System.Collections.Generic;
using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers.Colors;

public sealed class WeaponSkinWriter(WriterData data)
{
    public void WriteTo(string path, string baseWeapon)
    {
        string weaponName = Utils.BASE_WEAPON_NAME[baseWeapon];
        List<WeaponSkinType> defaults = [];

        using StreamWriter writer = new(path);
        writer.Write("The following is a list of all [[");
        writer.Write(weaponName);
        writer.WriteLine("]] skins in [[Brawlhalla]].");
        writer.WriteLine();

        writer.Write("==");
        writer.Write(weaponName);
        writer.WriteLine(" Skins==");

        writer.WriteLine("{{itembox/top}}");
        foreach (WeaponSkinType weaponSkin in data.WeaponSkinTypes.WeaponSkins)
        {
            if (
                weaponSkin.WeaponSkinName == "Template" ||
                weaponSkin.BaseWeapon != baseWeapon ||
                weaponSkin.DisplayNameKey is null ||
                weaponSkin.WeaponSkinName.EndsWith("Stub") ||
                weaponSkin.WeaponSkinName.EndsWith("EivorMale") ||
                weaponSkin.WeaponSkinName.EndsWith("Stance") ||
                // only show level 3
                weaponSkin.UpgradesTo is not null
            ) continue;

            if (weaponSkin.OwnerHero is not null)
            {
                defaults.Add(weaponSkin);
                continue;
            }

            ProcessWeaponSkinType(weaponSkin, writer);
        }
        writer.WriteLine("{{itembox/bottom}}");
        writer.WriteLine();

        writer.Write("==Default ");
        writer.Write(weaponName);
        writer.WriteLine(" Skins==");
        writer.Write("These ");
        writer.Write(weaponName);
        writer.WriteLine(" skins belong to default Legend skins. Because of this, they can only be equipped by the Legend they belong to.");
        writer.WriteLine();

        writer.WriteLine("{{itembox/top}}");
        foreach (WeaponSkinType weaponSkin in defaults)
        {
            ProcessWeaponSkinType(weaponSkin, writer);
        }
        writer.WriteLine("{{itembox/bottom}}");
    }

    private void ProcessWeaponSkinType(WeaponSkinType weaponSkin, StreamWriter writer)
    {
        DescType descType;

        (string weaponSkinName, string imageName, string displayName, bool isAnimated) = data.GetWeaponSkinNameParams(weaponSkin, false);
        writer.Write("{{itembox|width=");
        // width
        writer.Write(weaponSkin.BaseWeapon switch
        {
            "Axe" => 200,
            "Boots" => 200,
            "Bow" => 200,
            "Cannon" => 200,
            "Chakram" => 200,
            "Fists" => 200,
            "Greatsword" => 200,
            "Hammer" => 200,
            "Katar" => 200,
            "Orb" => 200,
            "Pistol" => 180,
            "RocketLance" => 200,
            "Scythe" => 200,
            "Spear" => 200,
            "Sword" => 200,
            _ => 0,
        });
        writer.Write("|height=");
        // height
        writer.Write(weaponSkin.BaseWeapon switch
        {
            "Axe" => 170,
            "Boots" => 170,
            "Bow" => 170,
            "Cannon" => 170,
            "Chakram" => 170,
            "Fists" => 170,
            "Greatsword" => 170,
            "Hammer" => 170,
            "Katar" => 170,
            "Orb" => 170,
            "Pistol" => 170,
            "RocketLance" => 170,
            "Scythe" => 170,
            "Spear" => 170,
            "Sword" => 170,
            _ => 0,
        });
        writer.Write("|name=");
        writer.Write(weaponSkinName);
        if (weaponSkinName != displayName)
        {
            writer.Write("|displayname=");
            writer.Write(displayName);
        }
        writer.Write("|image=");
        writer.Write(imageName);
        writer.Write(isAnimated ? ".gif" : ".png");
        // from a legend skin
        if (data.GetWeaponSkinSourceCostume(weaponSkin) is CostumeType costume)
        {
            descType = DescType.Desc;
            writer.Write("|desc=[[");
            (string costumeName, _, _) = data.GetSkinNameParams(costume);
            writer.Write(costumeName);
            writer.Write("]]");
        }
        // store
        else if (data.StoreTypes.ItemToStoreType.TryGetValue($"WeaponSkin {weaponSkin.WeaponSkinName}", out StoreType? storeType))
        {
            descType = DescType.Cost;
            writer.Write("|cost={{Coin|");
            // costs gold
            if (storeType.GoldCost > 0)
            {
                writer.Write("gold|");
                writer.Write(storeType.GoldCost);
            }
            // costs mammoth coins
            else if (storeType.IdolCost > 0)
            {
                writer.Write("mammoth|");
                writer.Write(storeType.IdolCost);
            }
            // costs glory
            else if (storeType.RankedPointsCost > 0)
            {
                writer.Write("glory|");
                writer.Write(storeType.RankedPointsCost);
            }
            // unexpected
            else
            {
                writer.Write("ERROR|0");
            }
            writer.Write("}}");

            if (storeType.EndDateKey is not null)
            {
                writer.Write("<br>");
                writer.Write(storeType.EndDateKey switch
                {
                    "StoreType_EndDate_RequiresSkyforged" => "+ Skyforged Variant",
                    "StoreType_EndDate_LimitedTime" => "Limited time purchase",
                    "StoreType_EndDate_Unavailable" => "Limited time purchase",
                    _ => "ERROR",
                });
            }
        }
        // pack
        else if (data.EntitlementTypes.WeaponSkinToEntitlement.TryGetValue(weaponSkin.WeaponSkinName, out EntitlementType? entitlement))
        {
            string packName;
            switch (entitlement.EntitlementName)
            {
                case "SpringPack":
                    packName = "Spring Championship 2017 Pack";
                    break;
                case "SummerPack":
                    packName = "Summer Championship 2017 Pack";
                    break;
                case "CollectorsRewards":
                    packName = "Collectors Pack";
                    break;
                default:
                    packName = data.LangFile.Entries[entitlement.DisplayNameKey!];
                    packName = packName.Trim('!');
                    if (!packName.EndsWith("Pack")) packName += " Pack";
                    break;
            }

            descType = DescType.Desc;
            writer.Write("|desc=[[");
            writer.Write(packName);
            writer.Write("]]");
        }
        // misc
        else
        {
            descType = DescType.Desc;
            writer.Write("|desc=");
            writer.Write(data.GetWeaponSkinMiscDesc(weaponSkin));
        }

        writer.Write("|");
        writer.Write(descType switch
        {
            DescType.Desc => "desc",
            DescType.Cost => "cost",
            _ => "ERROR",
        });
        writer.Write("height=");
        // desc/cost height
        writer.Write(weaponSkin.BaseWeapon switch
        {
            "Axe" => 55,
            "Boots" => 55,
            "Bow" => 55,
            "Cannon" => 55,
            "Chakram" => 55,
            "Fists" => 55,
            "Greatsword" => 55,
            "Hammer" => 55,
            "Katar" => 55,
            "Orb" => 55,
            "Pistol" => 55,
            "RocketLance" => 55,
            "Scythe" => 55,
            "Spear" => 55,
            "Sword" => 55,
            _ => 0,
        });
        writer.WriteLine("px}}");
    }

    enum DescType
    {
        ERROR,
        Desc,
        Cost
    }
}