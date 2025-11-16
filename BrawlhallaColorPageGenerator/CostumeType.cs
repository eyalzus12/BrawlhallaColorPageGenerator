using System.Collections.Generic;
using System.IO;
using System.Linq;
using nietras.SeparatedValues;

namespace BrawlhallaColorPageGenerator;

public sealed class CostumeType
{
    public string CostumeName { get; set; }
    public string? OwnerHero { get; set; }
    public string? DisplayNameKey { get; set; }
    public int CostumeIndex { get; set; }
    public string? UpgradesTo { get; set; }

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
    public CostumeType[] Costumes { get; set; }
    public Dictionary<string, int> UpgradeLevel { get; set; }

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

                // this sucks
                string? upgradedCostumeType = costumeType.UpgradesTo;
                while (
                    upgradedCostumeType is not null &&
                    Costumes.FirstOrDefault((costume) => costume.CostumeName == upgradedCostumeType) is CostumeType costume
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