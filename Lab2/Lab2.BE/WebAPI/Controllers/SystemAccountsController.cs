using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SystemAccountsController : ControllerBase
{
    private readonly ISystemAccountService _systemAccountService;
    private readonly IConfiguration _configuration;

    public SystemAccountsController(
        ISystemAccountService systemAccountService,
        IConfiguration configuration)
    {
        _systemAccountService = systemAccountService;
        _configuration = configuration;
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] AccountRequestDTO loginDTO)
    {
        var account = await _systemAccountService.Login(
            loginDTO.Email,
            loginDTO.Password);

        if (account == null)
            return Unauthorized("Invalid email or password.");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, account.EmailAddress),
            new Claim("Role", account.Role.ToString()),
            new Claim("AccountId", account.AccountId.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

        var credential = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credential);

        var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new AccountResponseDTO
        {
            Token = generatedToken,
            Role = account.Role.ToString(),
            AccountId = account.AccountId.ToString()
        });
    }
}