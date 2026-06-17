using BusinessObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Repositories;
using Services;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<CosmeticInformation>("CosmeticInformations");
modelBuilder.EntitySet<CosmeticCategory>("CosmeticCategories");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFE", policy =>
    {
        policy.WithOrigins("https://localhost:7002")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .AddOData(options =>
    {
        options.Select()
            .Filter()
            .OrderBy()
            .Expand()
            .Count()
            .SetMaxTop(null)
            .AddRouteComponents("odata", modelBuilder.GetEdmModel());
    });

builder.Services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();
builder.Services.AddScoped<ICosmeticInformationRepository, CosmeticInformationRepository>();

builder.Services.AddScoped<ISystemAccountService, SystemAccountService>();
builder.Services.AddScoped<ICosmeticInformationService, CosmeticInformationService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "Role") &&
            context.User.FindFirst("Role")!.Value == "1"));

    options.AddPolicy("AdminOrStaffOrMember", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "Role") &&
            (
                context.User.FindFirst("Role")!.Value == "1" ||
                context.User.FindFirst("Role")!.Value == "3" ||
                context.User.FindFirst("Role")!.Value == "4"
            )));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowFE");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();