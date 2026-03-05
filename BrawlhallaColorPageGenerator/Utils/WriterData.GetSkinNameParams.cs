using System;
using System.Globalization;
using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaLangReader;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public (string skinName, string imageName, string displayName, bool isAnimated) GetSkinNameParams(CostumeType costumeType, bool colorMode)
    {
        string? displayNameKey = costumeType.DisplayNameKey;

        bool isAnimated = false;

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

        switch (costumeType.CostumeName)
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
            case "Mando":
                if(!colorMode) displayName = "The Mandalorian & Grogu";
                break;
        }

        if (CostumeTypes.UpgradeLevel.TryGetValue(costumeType.CostumeName, out int upgradeLevel) && upgradeLevel != 0)
        {
            if (colorMode) displayName = skinName + " (Lvl " + upgradeLevel + ")";
            if (colorMode || !isAnimated) imageName = skinName + " Level " + upgradeLevel;
        }

        // html escape for the template
        if (colorMode)
        {
            skinName = skinName.Replace(":", "&#58;");
            displayName = displayName.Replace(":", "&#58;");
        }

        // no : in image names
        imageName = imageName.Replace(":", "");

        return (skinName, imageName, displayName, isAnimated);
    }
}