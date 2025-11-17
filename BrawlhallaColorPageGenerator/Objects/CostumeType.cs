using System.Collections.Generic;
using System.IO;
using System.Linq;
using nietras.SeparatedValues;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class CostumeType
{
    public string CostumeName { get; }
    public string? OwnerHero { get; }
    public string? DisplayNameKey { get; }
    public int CostumeIndex { get; }
    public string? UpgradesTo { get; }

    public CostumeType(SepReader.Row row)
    {
        CostumeName = row["CostumeName"].ToString();

        OwnerHero = row["OwnerHero"].ToString();
        if (string.IsNullOrWhiteSpace(OwnerHero)) OwnerHero = null;

        DisplayNameKey = row["DisplayNameKey"].ToString();
        if (string.IsNullOrWhiteSpace(DisplayNameKey)) DisplayNameKey = null;

        CostumeIndex = row["CostumeIndex"].TryParse<int>() ?? 0;

        UpgradesTo = row["UpgradesTo"].ToString();
        if (string.IsNullOrWhiteSpace(UpgradesTo)) UpgradesTo = null;
    }
}

public sealed class CostumeTypes
{
    public CostumeType[] Costumes { get; }
    public Dictionary<string, CostumeType> CostumesMap { get; }
    public Dictionary<string, int> UpgradeLevel { get; }

    public CostumeTypes(string content)
    {
        using StringReader textReader = new(content);
        textReader.ReadLine(); // skip first line bullshit
        SepReaderOptions sepReaderOptions = Sep.New(',').Reader((opts) =>
        {
            return opts with
            {
                DisableColCountCheck = true,
            };
        });
        using SepReader csvReader = sepReaderOptions.From(textReader);
        Costumes = [.. csvReader.Enumerate((row) => new CostumeType(row))];
        CostumesMap = new(Costumes.Select((c) => new KeyValuePair<string, CostumeType>(c.CostumeName, c)));

        UpgradeLevel = [];
        Queue<CostumeType> leftover = new(Costumes);
        while (leftover.TryDequeue(out CostumeType? costumeType))
        {
            if (costumeType.CostumeName == "Template")
                continue;

            if (costumeType.UpgradesTo is null)
            {
                UpgradeLevel[costumeType.CostumeName] = 0;
                continue;
            }

            if (UpgradeLevel.TryGetValue(costumeType.UpgradesTo, out int existingLevel))
            {
                if (existingLevel == 0) existingLevel = 1;

                UpgradeLevel[costumeType.CostumeName] = existingLevel++;

                // go up the upgrade chain and update level
                string? upgradedCostumeType = costumeType.UpgradesTo;
                while (
                    upgradedCostumeType is not null &&
                    CostumesMap.TryGetValue(upgradedCostumeType, out CostumeType? costume)
                )
                {
                    UpgradeLevel[costume.CostumeName] = existingLevel++;
                    upgradedCostumeType = costume.UpgradesTo;
                }
            }
            else
            {
                leftover.Enqueue(costumeType);
            }
        }
    }
}