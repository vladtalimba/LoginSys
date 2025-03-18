using Microsoft.EntityFrameworkCore;
using LoginSys.Server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure cors policies.
builder.Services.AddCors(options =>
{
    options.AddPolicy("Default",
        builder =>
        {
            builder.SetIsOriginAllowed(origin =>
            {
                if (origin.ToLower().StartsWith("https://localhost")) return true;

                return false;
            })
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/*builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();*/

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddAuthentication().AddCookie(
        "LOGIN_DETAILS",
        options =>
        {
            options.LoginPath = "/login";
            options.Cookie.Name = "LOGIN_DETAILS";
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
            options.SlidingExpiration = true;
        } 
    );

builder.Services.AddAuthorization(
    options =>
    {
        // Could assign role requirements in order to access various pages inside the app.
        options.AddPolicy("loggedin", policy => policy.RequireClaim("loggedin", "true"));
    });

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{/*
    app.UseSwagger();
    app.UseSwaggerUI();*/
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
