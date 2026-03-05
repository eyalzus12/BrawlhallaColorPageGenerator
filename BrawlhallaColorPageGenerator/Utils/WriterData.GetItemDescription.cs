using System;
using System.Collections.Generic;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public ItemDescription GetItemDescription(string itemName, ItemTypeEnum itemType)
    {
        // get store type
        string itemTypeString = itemType switch
        {
            ItemTypeEnum.Costume => "Costume",
            ItemTypeEnum.WeaponSkin => "WeaponSkin",
            _ => throw new ArgumentException("Invalid item type"),
        };
        StoreType? storeType = StoreTypes.ItemToStoreType.GetValueOrDefault($"{itemTypeString} {itemName}");

        string description;
        DescriptionTypeEnum descriptionType;

        // override description
        if (MISC_ITEM_DESCRIPTIONS.TryGetValue(itemName, out string? itemDescription))
        {
            descriptionType = DescriptionTypeEnum.Desc;
            description = itemDescription;
        }
        // weapon skin from a legend skin
        else if (itemType == ItemTypeEnum.WeaponSkin && GetWeaponSkinSourceCostume(itemName) is CostumeType costume)
        {
            descriptionType = DescriptionTypeEnum.Desc;
            (string costumeName, _, _, _) = GetSkinNameParams(costume, false);
            description = "[[" + costumeName + "]]";
        }
        // metadev skin
        else if (itemType == ItemTypeEnum.Costume && (itemName == "MDFait" || itemName == "MetadevNix" || itemName == "MetadevJaeyun" || CostumeTypes.CostumesMap[itemName].IsMetadev))
        {
            descriptionType = DescriptionTypeEnum.Desc;
            description = "Not normally obtainable.<br>See [[Metadev]].";
        }
        // chest exclusive
        else if (GetItemChestExclusive(itemName) is string chestName)
        {
            descriptionType = DescriptionTypeEnum.Desc;
            description = "{{ItemTag|chest|" + chestName + "}}";
        }
        // store
        else if (storeType is not null)
        {
            descriptionType = DescriptionTypeEnum.Cost;
            description = GetStoreTypeDescription(storeType, smallItemTag: itemType switch
            {
                ItemTypeEnum.Costume => false,
                ItemTypeEnum.WeaponSkin => true,
                _ => false,
            });
        }
        else if (GetItemPackExclusive(itemName, itemType) is string packName)
        {
            descriptionType = DescriptionTypeEnum.Desc;
            description = "[[" + packName + "]]";
        }
        // unknown
        else
        {
            descriptionType = DescriptionTypeEnum.Desc;
            description = "UNKNOWN";
        }

        RarityEnum rarity = storeType is not null ? storeType.Rarity switch
        {
            "Epic" or "EpicCrossover" => RarityEnum.Epic,
            "Mythic" => RarityEnum.Mythic,
            _ => RarityEnum.None,
        } : RarityEnum.None;

        // battlepass epic skins
        if (itemType == ItemTypeEnum.Costume && EpicBattlepassSkins.Contains(itemName))
        {
            rarity = RarityEnum.Epic;
        }

        return new()
        {
            Description = description,
            DescriptionType = descriptionType,
            Rarity = rarity,
        };
    }

    private static readonly HashSet<string> EpicBattlepassSkins = [
        "DemonQueen",
        "EpicNix",
        "EpicBrynn",
        "EpicDiana",
        "EpicOrion",
        "EpicEmber",
        "EpicWarlock",
        "EpicMordex",
        "EpicRaptor",
        "EgyptianShoujo",
        "EpicWitch",
    ];
}

public enum ItemTypeEnum
{
    Costume,
    WeaponSkin,
}

