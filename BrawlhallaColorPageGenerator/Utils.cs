using System.Collections.Generic;

namespace BrawlhallaColorPageGenerator;

public static class Utils
{
    public static Dictionary<string, string> BASE_WEAPON_NAME { get; } = new()
    {
        ["Axe"] = "Axe",
        ["Boots"] = "Battle Boots",
        ["Bow"] = "Bow",
        ["Cannon"] = "Cannon",
        ["Chakram"] = "Chakram",
        ["Fists"] = "Gauntlets",
        ["Greatsword"] = "Greatsword",
        ["Hammer"] = "Hammer",
        ["Katar"] = "Katars",
        ["Orb"] = "Orb",
        ["Pistol"] = "Blasters",
        ["RocketLance"] = "Rocket Lance",
        ["Scythe"] = "Scythe",
        ["Spear"] = "Spear",
        ["Sword"] = "Sword",
    };

    public static string[] WEAPON_LIST { get; } = [
        "Axe",
        "Battle Boots",
        "Bow",
        "Cannon",
        "Chakram",
        "Gauntlets",
        "Greatsword",
        "Hammer",
        "Katars",
        "Orb",
        "Blasters",
        "Rocket Lance",
        "Scythe",
        "Spear",
        "Sword",
    ];
}