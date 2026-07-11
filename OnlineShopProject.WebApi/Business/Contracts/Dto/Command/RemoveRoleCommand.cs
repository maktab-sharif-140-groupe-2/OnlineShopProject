namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Command;

public record RemoveRoleCommand(Guid RoleId, Guid RequesterId);
