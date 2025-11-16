namespace BrawlhallaColorPageGenerator;

public static class Utils
{
    public static string Apply(this string content, string target) => content.Replace("@", target);
    public static string Apply2(this string content, string target, string target2) => content.Apply(target).Replace("#", target2);
    public static string Apply3(this string content, string target, string target2, string target3) => content.Apply2(target, target2).Replace("&", target3);
    public static string Apply(this string content, char target) => content.Replace('@', target);
}