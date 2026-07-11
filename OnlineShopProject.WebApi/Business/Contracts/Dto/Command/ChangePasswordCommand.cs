using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Command;

public record ChangePasswordCommand(string UserId,string newPassword, string CurrentPassword);


