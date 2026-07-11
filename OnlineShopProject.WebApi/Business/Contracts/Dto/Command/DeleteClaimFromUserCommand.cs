namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Command;

public record DeleteClaimFromUserCommand(Guid UserId, string claimType, string claimValue, Guid RequesterId);

