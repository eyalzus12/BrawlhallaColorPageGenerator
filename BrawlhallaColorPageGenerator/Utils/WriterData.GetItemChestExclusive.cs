using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public string? GetItemChestExclusive(string itemName)
    {
        if (ChanceBoxTypes.ExclusiveItemToChanceBox.TryGetValue(itemName, out ChanceBoxType? chanceBox))
        {
            return LangFile.Entries[chanceBox.DisplayNameKey];
        }
        else
        {
            return null;
        }
    }
}