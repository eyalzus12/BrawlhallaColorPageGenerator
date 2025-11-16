using System.IO;
using nietras.SeparatedValues;

namespace BrawlhallaColorPageGenerator;

public sealed class CostumeType
{
    public string CostumeName { get; set; }
    public string? OwnerHero { get; set; }
    public string? DisplayNameKey { get; set; }
    public int CostumeIndex { get; set; }

    public CostumeType(SepReader.Row row)
    {
        CostumeName = row["CostumeName"].ToString();

        OwnerHero = row["OwnerHero"].ToString();
        if (string.IsNullOrWhiteSpace(OwnerHero)) OwnerHero = null;

        DisplayNameKey = row["DisplayNameKey"].ToString();
        if (string.IsNullOrWhiteSpace(DisplayNameKey)) DisplayNameKey = null;

        CostumeIndex = row["CostumeIndex"].TryParse<int>() ?? 0;
    }
}

public sealed class CostumeTypes
{
    public CostumeType[] Costumes { get; set; }

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
    }
}