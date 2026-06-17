using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagementRazorPages.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ProductManagementRazorPages.Pages.Cosmetics;

public class DetailsModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DetailsModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public CosmeticInformationVM? Cosmetic { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        var client = _httpClientFactory.CreateClient("CosmeticsAPI");
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        Cosmetic = await client.GetFromJsonAsync<CosmeticInformationVM>(
            $"/api/CosmeticInformations/{id}");

        return Page();
    }
}