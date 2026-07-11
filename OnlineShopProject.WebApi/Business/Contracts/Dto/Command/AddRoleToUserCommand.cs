namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Command;

public record AddRoleToUserCommand(Guid RoleId, Guid UserId, Guid RequesterId);
