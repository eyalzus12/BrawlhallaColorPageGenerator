using System;
using System.Collections.Generic;
using System.Globalization;
using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaLangReader;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public (string skinName, string imageName, string displayName, ImageExtensionEnum extension) GetSkinNameParams(CostumeType costumeType, bool colorMode)
    {
        string? displayNameKey = costumeType.DisplayNameKey;

        bool useLevelSuffix = true;
        ImageExtensionEnum extension = ImageExtensionEnum.Png;

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
                if (!colorMode) displayName = "The Mandalorian & Grogu";
                break;
            case "Heatblast":
                if (!colorMode)
                {
                    imageName = "AniHeatblast (lock-in)";
                    extension = ImageExtensionEnum.Gif;
                }
                break;
            case "Stevonnie":
            case "Diamondhead":
            case "FourArms":
            case "Leo":
            case "Donnie":
            case "Raph":
            case "Mikey":
            case "TaiLung":
                if (!colorMode) imageName += " (lock-in)";
                break;
            // misc epics
            case "Bubblegum":
            case "Spongebob":
                if (!colorMode)
                {
                    extension = ImageExtensionEnum.Gif;
                    imageName = "Ani" + imageName;
                }
                break;
            // bp 1
            case "Demon01":
            case "Demon02":
            case "Demon03":
            // bp 4
            case "WolfMonster01":
            case "WolfMonster02":
            case "WolfMonster03":
            // bp 11
            case "BP11Azoth01":
            case "BP11Azoth02":
            case "BP11Azoth03":
                if (!colorMode)
                {
                    useLevelSuffix = false;
                    extension = ImageExtensionEnum.Gif;
                    imageName = "Ani" + imageName;
                }
                break;
            // others
            case "CyberSam":
            case "PaleRider":
            // bp 1
            case "DemonQueen":
            // bp 2
            case "Synth01":
            case "Synth02":
            case "Synth03":
            // bp 3
            case "MakoProgression01":
            case "MakoProgression02":
            case "MakoProgression03":
            // bp 5
            case "BP5DualArt":
            case "BP5DualArt02":
            case "BP5DualArt03":
            // bp 6
            case "ElderDragon1":
            case "ElderDragon2":
            case "ElderDragon3":
            // bp 7
            case "T1Paladin":
            case "T2Paladin":
            case "T3Paladin":
            // bp 8
            case "TerminusLuchador01":
            case "TerminusLuchador02":
            case "TerminusLuchador03":
            // bp 9
            case "Guardian01":
            case "Guardian02":
            case "MonkGuardian03":
            // bp 10
            case "MagicalTeros01":
            case "MagicalTeros02":
            case "MagicalTeros03":
            // bp 12
            case "ShinobiBP1201":
            case "ShinobiBP1202":
            case "ShinobiBP1203":
            // bp 13
            case "ImugiDragon1":
            case "ImugiDragon2":
            case "ImugiDragon3":
                if (!colorMode)
                {
                    useLevelSuffix = false;
                    extension = ImageExtensionEnum.Webp;
                }
                break;
        }

        if (CostumeTypes.UpgradeLevel.TryGetValue(costumeType.CostumeName, out int upgradeLevel) && upgradeLevel != 0)
        {
            if (colorMode) displayName = skinName + " (Lvl " + upgradeLevel + ")";
            if (colorMode && useLevelSuffix) imageName = skinName + " Level " + upgradeLevel;
        }

        // names that are too long
        if (!colorMode)
        {
            if (_longCostumeNames.Contains(costumeType.CostumeName))
                displayName = "{{small|" + displayName + "}}";
        }

        // html escape for the template
        if (colorMode)
        {
            skinName = skinName.Replace(":", "&#58;");
            displayName = displayName.Replace(":", "&#58;");
        }

        // no : in image names
        imageName = imageName.Replace(":", "");

        return (skinName, imageName, displayName, extension);
    }

    private static readonly HashSet<string> _longCostumeNames = [
        "AnnivRoland",
    ];
}