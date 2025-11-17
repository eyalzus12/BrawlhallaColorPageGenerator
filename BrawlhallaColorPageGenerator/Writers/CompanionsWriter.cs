using System.IO;
using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaLangReader;

namespace BrawlhallaColorPageGenerator.Writers;

public sealed class CompanionsWriter(CompanionTypes companionTypes, LangFile langFile)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine("<includeonly><onlyinclude>");
        writer.WriteLine("The following is a list of all companions in {{{1|}}}. ''Click an image to view it in higher resolution.''");
        writer.WriteLine();
        writer.WriteLine("{{itembox/top}}");
        foreach (CompanionType companion in companionTypes.Companions)
        {
            if (companion.CompanionName == "Template") continue;

            (string companionName, string imageName, string displayName) = GetNameParams(companion);

            writer.WriteLine("{{itembox|width=150|height=150|name=¹²|image=Companion ³ Idle {{{1|}}}.png|compact=true|noimglink=true}}".Apply3(
                companionName,
                companionName == displayName ? "" : ("|displayname=" + displayName),
                imageName
            ));
        }
        writer.WriteLine("{{itembox/bottom}}");

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

        imageName = imageName.Replace(":", "");

        return (companionName, imageName, displayName);
    }
}