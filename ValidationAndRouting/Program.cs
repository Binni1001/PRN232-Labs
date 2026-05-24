using ValidationAndRouting.Contracts;
using ValidationAndRouting.Extensions;
using ValidationAndRouting.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();