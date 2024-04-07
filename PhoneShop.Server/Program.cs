using Microsoft.EntityFrameworkCore;
using PhoneShop.Server.Data;
using PhoneShop.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Starting connection
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string not found!"));
});
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<ICategory, CategoryRepository>();

//Ending...

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
