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
        bool longName = data.GetWeaponSkinNameIsLong(weaponSkin);

        DescType descType;

        (string weaponSkinName, string imageName, string displayName) = data.GetWeaponSkinNameParams(weaponSkin, false);
        writer.Write("{{itembox|width=200|height=170|name=");
        writer.Write(weaponSkinName);
        if (weaponSkinName != displayName || longName)
        {
            writer.Write("|displayname=");
            if (longName) writer.Write("{{small|");
            writer.Write(displayName);
            if (longName) writer.Write("}}");
        }
        writer.Write("|image=");
        writer.Write(imageName);
        writer.Write(".png");
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
                    _ => "UNKNOWN",
                });
            }
        }
        // pack
        else if (data.EntitlementTypes.WeaponSkinToEntitlement.TryGetValue(weaponSkin.WeaponSkinName, out EntitlementType? entitlement))
        {
            string packName = entitlement.EntitlementName switch
            {
                "SpringPack" => "Spring Championship 2017 Pack",
                "CollectorsRewards" => "Collectors Pack",
                _ => data.LangFile.Entries[entitlement.DisplayNameKey!],
            };
            packName = packName.Trim('!');
            if (!packName.EndsWith("Pack")) packName += " Pack";
            packName = packName switch
            {
                "Summer Champ Pack" => "Summer Championship 2017 Pack",
                _ => packName,
            };

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
        writer.WriteLine("height=55px}}");
    }

    enum DescType
    {
        ERROR,
        Desc,
        Cost
    }
}