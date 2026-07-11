namespace OnlineShopProject.WebApi.EndPoint.Dto.ResponseDto;

public record UserClaimResponseDto(Guid UserId, string ClaimType, string ClaimValue);

