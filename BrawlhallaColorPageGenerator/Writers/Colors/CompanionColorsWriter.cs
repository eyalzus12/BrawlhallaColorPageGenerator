using System.IO;
using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaLangReader;

namespace BrawlhallaColorPageGenerator.Writers.Colors;

public sealed class CompanionColorsWriter(CompanionTypes companionTypes, LangFile langFile)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine("<includeonly><onlyinclude>");
        writer.WriteLine("The following is a list of all companions in {{{1|}}}. ''Click an image to view it in higher resolution.''");
        writer.WriteLine();
        writer.WriteLine("{{List to itembox|color={{{1|}}}|");
        foreach (CompanionType companion in companionTypes.Companions)
        {
            if (companion.CompanionName == "Template") continue;

            (string companionName, string imageName, string displayName) = GetNameParams(companion);

            writer.Write(companionName);
            if (companionName != displayName)
            {
                writer.Write(" && displayname:");
                writer.Write(displayName);
            }
            writer.Write(" && image:Companion ");
            writer.Write(imageName);
            writer.WriteLine(" Idle $1.png");
        }
        writer.WriteLine("}}");

        writer.WriteLine("[[Category:Companions in all colors]]</onlyinclude></includeonly>");
        writer.WriteLine("<noinclude>");
        writer.WriteLine("{{doc}}");
        writer.WriteLine("</noinclude>");
    }

    private (string companionName, string imageName, string displayName) GetNameParams(CompanionType companionType)
    {
        // string companion = companionType.CompanionName;
        string displayNameKey = companionType.DisplayNameKey;

        string companionName = langFile.Entries[displayNameKey];
        string imageName = companionName;
        string displayName = companionName;

        companionName = companionName.Replace(":", "&#58;");
        displayName = displayName.Replace(":", "&#58;");
        imageName = imageName.Replace(":", "");

        return (companionName, imageName, displayName);
    }
}