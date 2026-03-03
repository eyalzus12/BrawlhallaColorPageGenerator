using System.Collections.Generic;
using System.IO;
using System.Linq;
using nietras.SeparatedValues;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class CostumeType
{
    public string CostumeName { get; }
    public string OwnerHero { get; }
    public string? DisplayNameKey { get; }
    public int CostumeIndex { get; }
    public string? UpgradesTo { get; }
    public string? WeaponSet { get; }
    public bool DoesNotOwnWeaponSet { get; }

    public CostumeType(SepReader.Row row)
    {
        CostumeName = row[nameof(CostumeName)].ToString();

        OwnerHero = row[nameof(OwnerHero)].ToString();

        DisplayNameKey = row[nameof(DisplayNameKey)].ToString();
        if (string.IsNullOrWhiteSpace(DisplayNameKey)) DisplayNameKey = null;

        CostumeIndex = row[nameof(CostumeIndex)].TryParse<int>() ?? 0;

        UpgradesTo = row[nameof(UpgradesTo)].ToString();
        if (string.IsNullOrWhiteSpace(UpgradesTo)) UpgradesTo = null;

        WeaponSet = row[nameof(WeaponSet)].ToString();
        if (string.IsNullOrWhiteSpace(WeaponSet)) WeaponSet = null;

        DoesNotOwnWeaponSet = row[nameof(DoesNotOwnWeaponSet)].TryParse<bool>() ?? false;
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
        CostumesMap = Costumes.ToDictionary((c) => c.CostumeName);

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