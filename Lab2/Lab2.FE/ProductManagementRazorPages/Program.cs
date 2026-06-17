var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSession();

builder.Services.AddHttpClient("CosmeticsAPI", client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ApiSettings:BaseUrl"]!);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Login");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();