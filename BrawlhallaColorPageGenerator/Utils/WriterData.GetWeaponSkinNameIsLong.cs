using System.Collections.Generic;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    private static readonly HashSet<string> _longWeaponSkinNameSet = [
        "Brave Magi ☆ Twinkling Justice",
        "Chocolate Cherry Sunburst",
        "World Cleaver",
    ];

    public bool GetWeaponSkinNameIsLong(WeaponSkinType weaponSkin)
    {
        string displayNameKey = weaponSkin.DisplayNameKey!;
        string weaponSkinName = LangFile.Entries[displayNameKey];
        return _longWeaponSkinNameSet.Contains(weaponSkinName);
    }
}