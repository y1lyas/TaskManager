using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TaskManager.Data;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Swagger ve Swagger UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controller servisleri
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });



var app = builder.Build();

// Geli�tirme ortam�ysa Swagger�� a�
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS ve yetkilendirme
app.UseHttpsRedirection();
app.UseAuthorization();

// Controller route'lar�n� kullan
app.MapControllers();

app.Run();
