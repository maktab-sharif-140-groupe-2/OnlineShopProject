namespace OnlineShopProject.WebApi.Contracts.JwtSetting;

public class JwtSettings
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public int ExpiresInMinutes { get; set; }
    public string EncryptKey { get; set; } = string.Empty;
}
