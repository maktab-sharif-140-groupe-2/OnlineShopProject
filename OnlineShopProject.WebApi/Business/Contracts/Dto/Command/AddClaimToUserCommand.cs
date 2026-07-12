namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Command;

public record AddClaimToUserCommand(Guid UserId, string ClaimType, string ClaimValue, Guid RequesterId);

