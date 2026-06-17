namespace WebAPI;

public class AccountRequestDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class AccountResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
}