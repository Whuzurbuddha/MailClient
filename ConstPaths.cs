using Microsoft.VisualBasic.FileIO;

namespace MailClient;

public static class ConstPaths
{
    public static readonly string? CachePath = $@"{SpecialDirectories.MyDocuments}\MailClient\\Cache";
    public static readonly string? LoginChachePath = $@"{CachePath}\LoginCache.json";
    public static readonly string? MainDirectory = $@"{SpecialDirectories.MyDocuments}\MailClient";
    public static readonly string? MailAccounts = $@"{SpecialDirectories.MyDocuments}\MailClient\mailaccounts";
}