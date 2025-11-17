using System.Collections.Generic;

namespace BrawlhallaColorPageGenerator;

public static class Utils
{
    public static readonly Dictionary<string, string> BASE_WEAPON_NAME = new()
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

    public static string Apply(this string content, string target) => content.Replace("¹", target);
    public static string Apply2(this string content, string target, string target2) => content.Apply(target).Replace("²", target2);
    public static string Apply3(this string content, string target, string target2, string target3) => content.Apply2(target, target2).Replace("³", target3);
    public static string Apply(this string content, char target) => content.Replace('¹', target);
}