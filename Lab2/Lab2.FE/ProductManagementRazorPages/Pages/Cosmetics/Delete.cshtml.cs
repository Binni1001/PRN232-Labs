using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagementRazorPages.Helpers;
using ProductManagementRazorPages.Models;
using System.Net.Http.Json;

namespace ProductManagementRazorPages.Pages.Cosmetics;

public class DeleteModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DeleteModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public CosmeticInformationVM Cosmetic { get; set; } = new();

    public string ErrorMessage { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (HttpContext.Session.GetString("Role") != "1")
            return RedirectToPage("/Cosmetics/Index");

        var client = _httpClientFactory.CreateClient("CosmeticsAPI");

        if (!ApiClientHelper.AddBearerToken(HttpContext, client))
            return RedirectToPage("/Login");

        Cosmetic = await client.GetFromJsonAsync<CosmeticInformationVM>(
            $"/api/CosmeticInformations/{id}") ?? new();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (HttpContext.Session.GetString("Role") != "1")
            return RedirectToPage("/Cosmetics/Index");

        var client = _httpClientFactory.CreateClient("CosmeticsAPI");

        if (!ApiClientHelper.AddBearerToken(HttpContext, client))
            return RedirectToPage("/Login");

        var response = await client.DeleteAsync(
            $"/api/CosmeticInformations/{Cosmetic.CosmeticId}");

        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage = await response.Content.ReadAsStringAsync();
            return Page();
        }

        return RedirectToPage("Index");
    }
}