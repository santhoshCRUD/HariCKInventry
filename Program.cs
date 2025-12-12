using HariCKInventry.Data;
using HariCKInventry.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB connection: prefer environment variable for Render
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

// Convert Render-style DATABASE_URL if needed
if (connectionString.StartsWith("postgres://") || connectionString.StartsWith("postgresql://"))
{
    // postgres://user:pass@host:port/db
    var uri = new Uri(connectionString.Replace("postgres://", "postgresql://"));
    var userInfo = uri.UserInfo.Split(':');
    var npgsqlConn =
        $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.Trim('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";

    connectionString = npgsqlConn;
}

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(connectionString));

builder.Services.AddRazorPages();
builder.Services.AddSingleton<FileStorageService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();

// Ensure DB created/migrated on startup (optional in dev)
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     db.Database.Migrate();
// }

app.Run();