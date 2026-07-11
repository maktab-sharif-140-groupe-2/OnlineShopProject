namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Command;

public record AddClaimToUserCommand(Guid UserId, string claimType, string claimValue, Guid RequesterId);

