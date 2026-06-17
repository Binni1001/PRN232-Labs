using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagementRazorPages.Models;
using System.Net.Http.Json;

namespace ProductManagementRazorPages.Pages;

public class LoginModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LoginModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient("CosmeticsAPI");

        var response = await client.PostAsJsonAsync("/api/SystemAccounts/Login",
            new LoginRequest
            {
                Email = Email,
                Password = Password
            });

        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage = "Invalid email or password.";
            return Page();
        }

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (result == null)
        {
            ErrorMessage = "Login failed.";
            return Page();
        }

        if (result.Role == "2")
        {
            ErrorMessage = "Manager does not have permission.";
            return Page();
        }

        HttpContext.Session.SetString("Token", result.Token);
        HttpContext.Session.SetString("Role", result.Role);
        HttpContext.Session.SetString("AccountId", result.AccountId);

        return RedirectToPage("/Cosmetics/Index");
    }
}