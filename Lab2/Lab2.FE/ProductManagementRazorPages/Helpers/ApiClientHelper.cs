using System.Net.Http.Headers;

namespace ProductManagementRazorPages.Helpers;

public static class ApiClientHelper
{
    public static bool AddBearerToken(HttpContext httpContext, HttpClient client)
    {
        var token = httpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
            return false;

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return true;
    }
}