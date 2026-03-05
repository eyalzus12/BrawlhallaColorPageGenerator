namespace BrawlhallaColorPageGenerator;

public readonly struct ItemDescription
{
    public required string Description { get; init; }
    public required DescriptionTypeEnum DescriptionType { get; init; }
    public required RarityEnum Rarity { get; init; }
}

public enum DescriptionTypeEnum
{
    Desc,
    Cost,
}

public enum RarityEnum
{
    None,
    Epic,
    Mythic,
}