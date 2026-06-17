using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagementRazorPages.Helpers;
using ProductManagementRazorPages.Models;
using System.Net.Http.Json;

namespace ProductManagementRazorPages.Pages.Cosmetics;

public class EditModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EditModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public CosmeticInformationVM Cosmetic { get; set; } = new();

    public List<CosmeticCategoryVM> Categories { get; set; } = new();

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

        await LoadCategoriesAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (HttpContext.Session.GetString("Role") != "1")
            return RedirectToPage("/Cosmetics/Index");

        var client = _httpClientFactory.CreateClient("CosmeticsAPI");

        if (!ApiClientHelper.AddBearerToken(HttpContext, client))
            return RedirectToPage("/Login");

        var response = await client.PutAsJsonAsync(
            $"/api/CosmeticInformations/{Cosmetic.CosmeticId}", Cosmetic);

        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage = await response.Content.ReadAsStringAsync();
            await LoadCategoriesAsync();
            return Page();
        }

        return RedirectToPage("Index");
    }

    private async Task LoadCategoriesAsync()
    {
        var client = _httpClientFactory.CreateClient("CosmeticsAPI");

        if (!ApiClientHelper.AddBearerToken(HttpContext, client))
            return;

        Categories = await client.GetFromJsonAsync<List<CosmeticCategoryVM>>(
            "/api/CosmeticCategories") ?? new();
    }
}