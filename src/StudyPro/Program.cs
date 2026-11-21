using Microsoft.EntityFrameworkCore;
using StudyPro.Components;
using StudyPro.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddHttpClient();

var connectionString = builder.Configuration.GetConnectionString("StudyProConnection")
    ?? throw new InvalidOperationException("Connection string 'StudyProConnection' not found.");

builder.Services.AddSqlite<StudyProContext>(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Initialize the database
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StudyProContext>();
    if (await db.Database.EnsureCreatedAsync())
    {
        await SeedData.InitializeAsync(db);
    }
}

await app.RunAsync();