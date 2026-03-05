using System;
using System.Collections.Generic;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public string? GetItemPackExclusive(string itemName, ItemTypeEnum itemType)
    {
        Dictionary<string, EntitlementType> itemToEntitlement = itemType switch
        {
            ItemTypeEnum.Costume => EntitlementTypes.CostumeToEntitlement,
            ItemTypeEnum.WeaponSkin => EntitlementTypes.WeaponSkinToEntitlement,
            _ => throw new ArgumentException("Invalid item type"),
        };

        if (itemToEntitlement.TryGetValue(itemName, out EntitlementType? entitlement))
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
                case "SummerPack23":
                    packName = "Summer Championship 2023 Pack";
                    break;
                case "FallPack18":
                    packName = "Autumn Championship 2018 Pack";
                    break;
                case "CollectorsRewards":
                    packName = "Collectors Pack";
                    break;
                default:
                    packName = LangFile.Entries[entitlement.DisplayNameKey!];
                    packName = packName.Trim('!');
                    if (!packName.EndsWith("Pack")) packName += " Pack";
                    break;
            }

            return packName;
        }
        else
        {
            return null;
        }
    }
}