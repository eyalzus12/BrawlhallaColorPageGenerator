using System.Text;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public string GetStoreTypeDescription(StoreType storeType, bool smallItemTag)
    {
        string FormatItemTag(string tag, int year = 0) => "{{ItemTag|" + tag + (smallItemTag ? "|small" : "") + (year > 0 ? "|" + year : "") + "}}";

        StringBuilder sb = new("{{Coin|");
        if (storeType.GoldCost > 0)
        {
            sb.Append("gold|");
            sb.Append(storeType.GoldCost);
        }
        // costs mammoth coins
        else if (storeType.IdolCost > 0)
        {
            sb.Append("mammoth|");
            sb.Append(storeType.StoreName switch
            {
                "PaleRider" => 300,
                "MythicWuShang" => 900,
                "MythicNix" => 900,
                _ => storeType.IdolCost
            });
        }
        // costs glory
        else if (storeType.RankedPointsCost > 0)
        {
            sb.Append("glory|");
            sb.Append(storeType.RankedPointsCost);
        }
        // costs tickets
        else if (storeType.SpecialCurrencyType is not null)
        {
            sb.Append("ticket ");
            sb.Append(storeType.SpecialCurrencyType switch
            {
                "BHFest25" => "fest",
                "Heatwave25" => "orange",
                "BackToSchool25" => "school",
                "Halloween25" => "halloween",
                "Anniversary25" => "anniv",
                "Christmas25" => "xmas",
                "VDay25" => "love",
                "StPatricks26" => "march",
                _ => "ERROR",
            });
            sb.Append('|');
            if (storeType.SpecialCurrencyCost > 0)
            {
                sb.Append(storeType.SpecialCurrencyCost);
            }
            // event-finish skin
            else
            {
                sb.Append(1850);
            }
        }
        // unexpected
        else
        {
            sb.Append("ERROR|0");
        }
        sb.Append("}}");

        if (storeType.SpecialCurrencyType is not null)
        {
            bool useSmallElement = storeType.SpecialCurrencyType switch
            {
                "BHFest25" => false,
                "Heatwave25" => false,
                "BackToSchool25" => true,
                "Halloween25" => true,
                "Anniversary25" => false,
                "Christmas25" => false,
                "VDay25" => false,
                "StPatricks26" => true,
                _ => false,
            };

            sb.Append("<br>");
            if (useSmallElement) sb.Append("<small>");
            sb.Append(storeType.SpecialCurrencyType switch
            {
                "BHFest25" => FormatItemTag("fest", 2025),
                "Heatwave25" => FormatItemTag("summer", 2025),
                "BackToSchool25" => FormatItemTag("school", 2025),
                "Halloween25" => FormatItemTag("halloween", 2025),
                "Anniversary25" => FormatItemTag("anniv", 2025),
                "Christmas25" => FormatItemTag("xmas", 2025),
                "VDay25" => FormatItemTag("love", 2026),
                "StPatricks26" => FormatItemTag("march", 2026),
                _ => " ERROR",
            });
            if (useSmallElement) sb.Append("</small>");
        }
        else if (storeType.EndDateKey is not null)
        {
            sb.Append("<br>");
            sb.Append(storeType.EndDateKey switch
            {
                "StoreType_EndDate_RequiresSkyforged" => "+ Skyforged Variant",
                "StoreType_EndDate_LimitedTime" => storeType.TimedPromotion switch
                {
                    "Valhallentines" => FormatItemTag("valentines"),
                    "StPatricks" => FormatItemTag("march"),
                    "SpringEvent" => FormatItemTag("spring"),
                    "Heatwave" => FormatItemTag("summer"),
                    "Halloween" => FormatItemTag("halloween"),
                    "BackToSchool" => FormatItemTag("school"),
                    "Anniversary" => FormatItemTag("anniversary"),
                    "Christmas" => FormatItemTag("winter"),

                    _ => "Limited time purchase",
                },
                "StoreType_EndDate_Unavailable" => "Limited time purchase",
                _ => "ERROR",
            });
        }

        return sb.ToString();
    }
}