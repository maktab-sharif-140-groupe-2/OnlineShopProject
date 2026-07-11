namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Query;

public record TokenLoginQuery(string AccessToken, double ExpiresIn);

