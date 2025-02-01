using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurant_API.Data;
using Restaurant_API.Mappings;
using Restaurant_API.MiddleWares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Restaurant_API.Repositories.Auth;
using Restaurant_API.Repositories.RMeal;
using Restaurant_API.Repositories.RDrink;
using Restaurant_API.Repositories.RDessert;
using Restaurant_API.Repositories.RSeat;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Restaurant API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantConnectionString")));
builder.Services.AddDbContext<RestaurantAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantAuthConnectionString")));

// Scopes
builder.Services.AddScoped<IMealRepository, SqlMealRepository>();
builder.Services.AddScoped<IDrinkRepository, SqlDrinkRepository>();
builder.Services.AddScoped<IDessertRepository, SqlDessertRepository>();
builder.Services.AddScoped<ISeatRepository, SqlSeatRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

// Mapping Part
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Token Part
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"])
            )
    });
builder.Services.AddDbContext<RestaurantAuthDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantAuthConnectionString")));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Restaurant")
    .AddEntityFrameworkStores<RestaurantAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapOpenApi();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
