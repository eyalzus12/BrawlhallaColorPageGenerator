namespace BrawlhallaColorPageGenerator;

public static class Utils
{
    public static string Apply(this string content, string target) => content.Replace("@", target);
    public static string Apply(this string content, char target) => content.Replace('@', target);
}