using System;
using System.Globalization;
using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaLangReader;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public (string skinName, string imageName, string displayName) GetSkinNameParams(CostumeType costumeType)
    {
        string costumeName = costumeType.CostumeName;
        string? displayNameKey = costumeType.DisplayNameKey;

        string skinName;
        if (displayNameKey is not null)
        {
            skinName = LangFile.Entries[displayNameKey];
        }
        else
        {
            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            string ownerHero = costumeType.OwnerHero;
            HeroType hero = HeroTypes.HeroesMap[ownerHero];
            ArgumentNullException.ThrowIfNull(hero.BioName);
            skinName = textInfo.ToTitleCase(hero.BioName);
        }

        string imageName = skinName;
        string displayName = skinName;

        switch (costumeName)
        {
            case "SnakeEyes":
                imageName = "Snake Eyes (Thatch Skin)";
                break;
            case "Eivor":
                displayName = imageName = "Eivor (Female)";
                break;
            case "EivorMale":
                displayName = imageName = "Eivor (Male)";
                break;
        }

        if (CostumeTypes.UpgradeLevel.TryGetValue(costumeName, out int upgradeLevel) && upgradeLevel != 0)
        {
            displayName = skinName + " (Lvl " + upgradeLevel + ")";
            imageName = skinName + " Level " + upgradeLevel;
        }

        skinName = skinName.Replace(":", "&#58;");
        displayName = displayName.Replace(":", "&#58;");
        imageName = imageName.Replace(":", "");

        return (skinName, imageName, displayName);
    }
}