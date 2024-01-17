using Microsoft.VisualBasic.FileIO;

namespace MailClient;

public static class ConstPaths
{
    public static readonly string? LoginChachePath = $@"{SpecialDirectories.MyDocuments}\MailClient\cache\LoginCache.json";
    public static readonly string? MainDirectory = $"{SpecialDirectories.MyDocuments}\\MailClient";
}