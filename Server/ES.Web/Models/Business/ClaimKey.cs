using Microsoft.IdentityModel.Tokens;

namespace ES.Web.Models;

public static class ClaimKey
{
    private static string Prefix = "Claim.Key";
    public static string Login { get; } = $"{Prefix}.Login";
    public static string FirstName { get; } = $"{Prefix}.FirstName";
    public static string LastName { get; } = $"{Prefix}.LastName";
    public static string Id { get; } = $"{Prefix}.Id";
    public static string Role { get; } = $"{Prefix}.Role";
    public static string CookiesId { get; } = "App.Cookies.Id";
}