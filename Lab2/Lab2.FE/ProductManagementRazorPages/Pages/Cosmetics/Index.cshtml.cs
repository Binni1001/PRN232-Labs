using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagementRazorPages.Helpers;
using ProductManagementRazorPages.Models;
using System.Net.Http.Json;

namespace ProductManagementRazorPages.Pages.Cosmetics;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<CosmeticInformationVM> Cosmetics { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? SearchName { get; set; }

    public string? Role { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Role = HttpContext.Session.GetString("Role");

        if (string.IsNullOrEmpty(Role))
            return RedirectToPage("/Login");

        var client = _httpClientFactory.CreateClient("CosmeticsAPI");

        if (!ApiClientHelper.AddBearerToken(HttpContext, client))
            return RedirectToPage("/Login");

        Cosmetics = await client.GetFromJsonAsync<List<CosmeticInformationVM>>(
            "/api/CosmeticInformations") ?? new();

        if (!string.IsNullOrWhiteSpace(SearchName))
        {
            Cosmetics = Cosmetics
                .Where(x => x.CosmeticName != null &&
                            x.CosmeticName.Contains(SearchName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        return Page();
    }
}