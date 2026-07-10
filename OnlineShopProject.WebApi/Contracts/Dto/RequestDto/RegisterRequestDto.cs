namespace OnlineShopProject.WebApi.Contracts.Dto.RequestDto;

public record RegisterRequestDto(string FullName, int Age, string Email, string PhoneNumber, string Password);

