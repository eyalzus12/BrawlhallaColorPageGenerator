using System.Collections.Generic;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    private const string VSR_DESCRIPTION = "[[Brawlhalla Viewership Rewards|Viewership Rewards]]";
    private const string GIFTS_OF_ASGARD_DESCRIPTION = "[[The Gifts of Asgard]]";
    private const string BUNDLE_EXCLUSIVE_DESCRIPTION = "[[Store Bundles|Bundle]] exclusive";
    private const string UNOBTAINABLE_DESCRIPTION = "''Unobtainable''";

    public readonly static Dictionary<string, string> MISC_ITEM_DESCRIPTIONS = new()
    {
        #region vsr
        // charity
        ["AxeCharity"] = VSR_DESCRIPTION,
        ["GreatswordCharity2022"] = VSR_DESCRIPTION,
        ["BowCharity2021"] = VSR_DESCRIPTION,
        ["CannonCharity2021"] = VSR_DESCRIPTION,
        ["HammerCharity"] = VSR_DESCRIPTION,
        ["KatarCharity2022"] = VSR_DESCRIPTION,
        ["OrbCharity2021"] = VSR_DESCRIPTION,
        ["RocketLanceCharity2021"] = VSR_DESCRIPTION,
        ["ScytheCharity"] = VSR_DESCRIPTION,
        ["SpearCharity2020"] = VSR_DESCRIPTION,
        // mahou shoujo
        ["HammerMagicalGirl"] = VSR_DESCRIPTION,
        ["OrbMagicalGirl"] = VSR_DESCRIPTION,
        ["RocketLanceMagicalGirl"] = VSR_DESCRIPTION,
        // neo city
        ["CannonEsports2026"] = VSR_DESCRIPTION,
        ["HammerEsports2026"] = VSR_DESCRIPTION,
        ["GreatswordEsports2026"] = VSR_DESCRIPTION,
        ["SpearEsports2026"] = VSR_DESCRIPTION,
        ["SwordEsports2026"] = VSR_DESCRIPTION,
        #endregion
        #region gifts of asgard
        ["AxeSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["PistolSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["FistsSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["KatarSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["ScytheSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["SpearSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["SwordSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        #endregion
        #region bundle exclusive
        ["CannonGjallahorn"] = BUNDLE_EXCLUSIVE_DESCRIPTION,
        #endregion
        #region unobtainable
        ["AxeGears"] = UNOBTAINABLE_DESCRIPTION,
        ["AxeFlame"] = UNOBTAINABLE_DESCRIPTION,
        ["PistolValkyrie"] = UNOBTAINABLE_DESCRIPTION,
        ["PistolGear"] = UNOBTAINABLE_DESCRIPTION,
        ["PistolSnakeGod"] = UNOBTAINABLE_DESCRIPTION,
        ["CannonMark1"] = UNOBTAINABLE_DESCRIPTION,
        ["HammerBigSteam"] = UNOBTAINABLE_DESCRIPTION,
        ["HammerWitch"] = UNOBTAINABLE_DESCRIPTION,
        ["HammerScrap"] = UNOBTAINABLE_DESCRIPTION,
        ["HammerHighwayman"] = UNOBTAINABLE_DESCRIPTION,
        ["HammerTitanium"] = UNOBTAINABLE_DESCRIPTION,
        ["HammerNinja"] = UNOBTAINABLE_DESCRIPTION,
        ["KatarEdge"] = UNOBTAINABLE_DESCRIPTION,
        ["KatarFuture"] = UNOBTAINABLE_DESCRIPTION,
        ["RocketLanceCyber"] = UNOBTAINABLE_DESCRIPTION,
        ["SpearGear"] = UNOBTAINABLE_DESCRIPTION,
        ["SwordNapoleon"] = UNOBTAINABLE_DESCRIPTION,
        ["SwordMech"] = UNOBTAINABLE_DESCRIPTION,
        ["SwordWitch"] = UNOBTAINABLE_DESCRIPTION,
        ["SwordBrass"] = UNOBTAINABLE_DESCRIPTION,
        ["SwordValkyrie"] = UNOBTAINABLE_DESCRIPTION,
        ["SwordSalvage"] = UNOBTAINABLE_DESCRIPTION,
        #endregion
        #region battlepass
        // bp 1
        ["AxeBattlePassSet"] = "{{BPReward|Season One|gold|Tier 29}}",
        ["PistolBattlePassSet"] = "{{BPReward|Season One|gold|Tier 50}}",
        ["BowBattlePassSet"] = "{{BPReward|Season One|gold|Tier 57}}",
        ["CannonBattlePassSet"] = "{{BPReward|Season One|free|Tier 6}}",
        ["FistsBattlePassSet"] = "{{BPReward|Season One|gold|Tier 65}}",
        ["GreatswordBattlePassSet"] = "{{BPReward|Season One Classic|gold|Tier 81}}",
        ["HammerBattlePassSet"] = "{{BPReward|Season One|gold|Tier 41}}",
        ["KatarBattlePassSet"] = "{{BPReward|Season One|gold|Tier 9}}",
        ["OrbBattlePassSet"] = "{{BPReward|Season One|gold|Tier 25}}",
        ["RocketLanceBattlePassSet"] = "{{BPReward|Season One|gold|Tier 19}}",
        ["ScytheBattlePassSet"] = "{{BPReward|Season One|gold|Tier 79}}",
        ["SpearBattlePassSet"] = "{{BPReward|Season One|gold|Tier 37}}",
        ["SwordBattlePassSet"] = "{{BPReward|Season One|gold|Tier 15}}",

        // bp 2
        ["AxeBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 15}}",
        ["BootsBattlepassSet2"] = "{{BPReward|Season Two Classic|free|Tier 62}}",
        ["PistolBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 41}}",
        ["BowBattlePassSet2"] = "{{BPReward|Season Two|free|Tier 6}}",
        ["CannonBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 57}}",
        ["FistsBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 50}}",
        ["GreatswordBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 81}}",
        ["HammerBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 9}}",
        ["KatarBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 25}}",
        ["OrbBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 65}}",
        ["RocketLanceBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 29}}",
        ["ScytheBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 37}}",
        ["SpearBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 79}}",
        ["SwordBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 19}}",

        // bp 3
        ["AxeBPJotun"] = "{{BPReward|Season Three|gold|Tier 37}}",
        ["BootsBPJotun"] = "{{BPReward|Season Three Classic|gold|Tier 69}}",
        ["PistolBPJotun"] = "{{BPReward|Season Three|gold|Tier 53}}",
        ["BowBPJotun"] = "{{BPReward|Season Three|gold|Tier 25}}",
        ["CannonBPJotun"] = "{{BPReward|Season Three|gold|Tier 41}}",
        ["FistsBPJotun"] = "{{BPReward|Season Three|gold|Tier 69|small}}<br>{{BPReward|Season Three Classic|free|Tier 66|small}}",
        ["GreatswordBPJotun"] = "{{BPReward|Season Three|gold|Tier 57}}",
        ["HammerBPJotun"] = "{{BPReward|Season Three|gold|Tier 77}}",
        ["KatarBPJotun"] = "{{BPReward|Season Three|free|Tier 6}}",
        ["OrbBPJotun"] = "{{BPReward|Season Three|gold|Tier 9}}",
        ["RocketLanceBPJotun"] = "{{BPReward|Season Three|gold|Tier 21}}",
        ["ScytheBPJotun"] = "{{BPReward|Season Three|gold|Tier 81}}",
        ["SpearBPJotun"] = "{{BPReward|Season Three|gold|Tier 15}}",
        ["SwordBPJotun"] = "{{BPReward|Season Three|gold|Tier 29}}",

        // bp 4
        ["AxeBP4Axe"] = "{{BPReward|Season Four|gold|Tier 21}}",
        ["BootsBP4"] = "{{BPReward|Season Four Classic|gold|Tier 69}}",
        ["PistolBP4Blaster"] = "{{BPReward|Season Four|gold|Tier 25}}",
        ["BowBP4Bow"] = "{{BPReward|Season Four|gold|Tier 29}}",
        ["CannonBP4Cannon"] = "{{BPReward|Season Four|gold|Tier 9}}",
        ["FistsBP4Gauntlet"] = "{{BPReward|Season Four|free|Tier 6}}",
        ["GreatswordBP4GreatSword"] = "{{small|{{BPReward|Season Four|gold|Tier 69}}<br>{{BPReward|Season Four Classic|free|Tier 66}}}}",
        ["HammerBP4Hammer"] = "{{BPReward|Season Four|gold|Tier 15}}",
        ["KatarBP4Katar"] = "{{BPReward|Season Four|gold|Tier 57}}",
        ["OrbBP4Orb"] = "{{BPReward|Season Four|gold|Tier 41}}",
        ["RocketLanceBP4RocketLance"] = "{{BPReward|Season Four|gold|Tier 37}}",
        ["ScytheBP4Scythe"] = "{{BPReward|Season Four|gold|Tier 53}}",
        ["SpearBP4Spear"] = "{{BPReward|Season Four|gold|Tier 81}}",
        ["SwordBP4Sword"] = "{{BPReward|Season Four|gold|Tier 77}}",

        // bp 5
        ["AxeBP5Axe"] = "{{BPReward|Season Five|gold|Tier 77}}",
        ["BPC5Boots"] = "{{BPReward|Season Five Classic|gold|Tier 69}}",
        ["PistolBP5Blaster"] = "{{BPReward|Season Five|free|Tier 6}}",
        ["BowBP5Bow"] = "{{BPReward|Season Five|gold|Tier 53}}",
        ["CannonBP5Cannon"] = "{{BPReward|Season Five|gold|Tier 41}}",
        ["FistsBP5Fists"] = "{{BPReward|Season Five|gold|Tier 25}}",
        ["GreatswordBP5Greatsword"] = "{{BPReward|Season Five|gold|Tier 21}}",
        ["HammerBP5Hammer"] = "{{BPReward|Season Five|gold|Tier 81}}",
        ["KatarBP5Katar"] = "{{BPReward|Season Five|gold|Tier 9}}",
        ["OrbBP5Orb"] = "{{BPReward|Season Five|gold|Tier 57}}",
        ["RocketLanceBP5RocketLance"] = "{{BPReward|Season Five|gold|Tier 29}}",
        ["ScytheBP5Scythe"] = "{{BPReward|Season Five|gold|Tier 37}}",
        ["SpearBP5Spear"] = "<span style=\"font-size:76%\">{{BPReward|Season Five|gold|Tier 69}}<br>{{BPReward|Season Five Classic|free|Tier 65}}</span>",
        ["SwordBP5Sword"] = "{{BPReward|Season Five|gold|Tier 15}}",

        // bp 6
        ["AxeBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 37}}",
        ["BootsBP6"] = "{{BPReward|Season Six Classic|gold|Tier 35}}",
        ["PistolBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 9}}",
        ["BowBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 29}}",
        ["CannonBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 57}}",
        ["ChakramBP6"] = "{{BPReward|Season Six Classic|gold|Tier 73}}",
        ["FistsBP6weaponset"] = "{{BPReward|Season Six|free|Tier 6}}",
        ["GreatswordBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 77}}",
        ["HammerBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 69}}",
        ["KatarBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 25}}",
        ["OrbBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 41}}",
        ["RocketLanceBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 15}}",
        ["ScytheBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 53}}",
        ["SpearBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 81}}",
        ["SwordBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 21}}",

        // bp 7
        ["AxeBP7"] = "{{BPReward|Season Seven|gold|Tier 53}}",
        ["BootsBP7"] = "{{BPReward|Season Seven|gold|Tier 9}}",
        ["PistolBP7"] = "{{BPReward|Season Seven|gold|Tier 17}}",
        ["BowBP7"] = "{{BPReward|Season Seven|gold|Tier 37}}",
        ["CannonBP7"] = "{{BPReward|Season Seven|gold|Tier 15}}",
        ["ChakramBP7"] = "{{BPReward|Season Seven Classic|gold|Tier 21}}",
        ["FistsBP7"] = "{{BPReward|Season Seven|gold|Tier 57}}",
        ["GreatswordBP7"] = "<span style=\"font-size:75%\">{{BPReward|Season Seven|gold|Tier 21}}<br>{{BPReward|Season Seven Classic|free|Tier 20}}</span>",
        ["HammerBP7"] = "{{BPReward|Season Seven|free|Tier 6}}",
        ["KatarBP7"] = "{{BPReward|Season Seven|gold|Tier 41}}",
        ["OrbBP7"] = "{{BPReward|Season Seven|gold|Tier 25}}",
        ["RocketLanceBP7"] = "{{BPReward|Season Seven|gold|Tier 29}}",
        ["ScytheBP7"] = "{{BPReward|Season Seven|gold|Tier 69}}",
        ["SpearBP7"] = "{{BPReward|Season Seven|gold|Tier 77}}",
        ["SwordBP7"] = "{{BPReward|Season Seven|gold|Tier 81}}",

        // bp 8
        ["AxeTerminus"] = "{{BPReward|Season Eight|gold|Tier 13}}",
        ["BootsTerminus"] = "{{BPReward|Season Eight|gold|Tier 63}}",
        ["PistolTerminus"] = "{{BPReward|Season Eight|gold|Tier 37}}",
        ["BowTerminus"] = "{{BPReward|Season Eight|gold|Tier 26}}",
        ["CannonTerminus"] = "{{BPReward|Season Eight|gold|Tier 18}}",
        ["FistsTerminus"] = "{{BPReward|Season Eight|gold|Tier 33}}",
        ["GreatswordTerminus"] = "{{BPReward|Season Eight|gold|Tier 58}}",
        ["HammerTerminus"] = "{{BPReward|Season Eight|gold|Tier 67}}",
        ["KatarTerminus"] = "{{BPReward|Season Eight|free|Tier 78}}",
        ["OrbTerminus"] = "{{BPReward|Season Eight|free|Tier 4}}",
        ["ScytheTerminus"] = "{{BPReward|Season Eight|gold|Tier 83}}",
        ["SwordTerminus"] = "{{BPReward|Season Eight|gold|Tier 73}}",

        // bp 9
        ["AxeDeathrider"] = "{{BPReward|Season Nine|gold|Tier 18}}",
        ["BootsDeathrider"] = "{{BPReward|Season Nine|gold|Tier 63}}",
        ["PistolDeathrider"] = "{{BPReward|Season Nine|gold|Tier 33}}",
        ["CannonDeathrider"] = "{{BPReward|Season Nine|gold|Tier 73}}",
        ["FistsDeathrider"] = "{{BPReward|Season Nine|free|Tier 78}}",
        ["GreatSwordDeathrider"] = "{{BPReward|Season Nine|gold|Tier 83}}",
        ["HammerDeathrider"] = "{{BPReward|Season Nine|free|Tier 4}}",
        ["KatarDeathrider"] = "{{BPReward|Season Nine|gold|Tier 67}}",
        ["OrbDeathrider"] = "{{BPReward|Season Nine|gold|Tier 37}}",
        ["RocketLanceDeathrider"] = "{{BPReward|Season Nine|gold|Tier 26}}",
        ["SpearDeathrider"] = "{{BPReward|Season Nine|gold|Tier 58}}",
        ["SwordDeathrider"] = "{{BPReward|Season Nine|gold|Tier 13}}",

        // bp 10
        ["AxeBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 26}}",
        ["BootsBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 33}}",
        ["PistolBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 37}}",
        ["BowBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 58}}",
        ["CannonBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 13}}",
        ["FistsBP10Mecha"] = "{{BPReward|Season Ten|free|Tier 78}}",
        ["KatarBP10Mecha"] = "{{BPReward|Season Ten|free|Tier 4}}",
        ["OrbBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 63}}",
        ["RocketlanceBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 18}}",
        ["ScytheBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 67}}",
        ["SpearBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 73}}",
        ["SwordBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 83}}",

        // bp 11
        ["AxeCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 73}}",
        ["BootsCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 58}}",
        ["PistolCatBP11"] = "{{BPReward|Season Eleven|free|Tier 4}}",
        ["BowCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 13}}",
        ["CannonCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 26}}",
        ["ChakramCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 73}}",
        ["FistsCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 45}}",
        ["GreatswordCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 18}}",
        ["HammerCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 63}}",
        ["OrbCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 37}}",
        ["RocketLanceCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 33}}",
        ["ScytheCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 83}}",
        ["SpearCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 67}}",

        // bp 12
        ["AxeBP12"] = "{{BPReward|Season Twelve|gold|Present Tier 6}}",
        ["BootsBP12"] = "{{BPReward|Season Twelve|gold|Past Tier 14}}",
        ["PistolBP12"] = "{{BPReward|Season Twelve|free|Present Tier 4}}",
        ["BowBP12"] = "{{BPReward|Season Twelve|free|Past Tier 23}}",
        ["CannonBP12"] = "{{BPReward|Season Twelve|free|Future Tier 1}}",
        ["ChakramBP12"] = "{{BPReward|Season Twelve|gold|Past Tier 22}}",
        ["GreatswordBP12"] = "{{BPReward|Season Twelve|free|Present Tier 23}}",
        ["HammerBP12"] = "{{BPReward|Season Twelve|gold|Past Tier 8}}",
        ["KatarBP12"] = "{{BPReward|Season Twelve|free|Intro Tier 5}}",
        ["RocketLanceBP12"] = "{{BPReward|Season Twelve|free|Past Tier 4}}",
        ["ScytheBP12"] = "{{BPReward|Season Twelve|gold|Future Tier 21}}",
        ["SpearBP12"] = "{{BPReward|Season Twelve|gold|Future Tier 4}}",
        ["SwordBP12"] = "{{BPReward|Season Twelve|free|Future Tier 23}}",

        #endregion
        #region misc
        ["SteelSeries"] = "SteelSeries 2022 reward",
        ["OrbLogitech"] = "Tournament Reward",
        #endregion
    };
}