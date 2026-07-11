namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Command;

public record RegisterCommand(string FullName, int Age, string Email, string PhoneNumber, string Password);

