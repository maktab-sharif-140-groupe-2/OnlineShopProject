using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineShopProject.WebApi.Authentications.Constants;
using OnlineShopProject.WebApi.Business.Contracts.JwtSetting;
using OnlineShopProject.WebApi.Business.Extensions;
using OnlineShopProject.WebApi.Business.Services.Implementation;
using OnlineShopProject.WebApi.Business.Services.Interface;
using OnlineShopProject.WebApi.Domain.Entities.RoleEntity;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity;
using OnlineShopProject.WebApi.Infrastructure.Data;
using OnlineShopProject.WebApi.Infrastructure.Repositories.Implementation;
using OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(option =>
option.AddPolicy("CorsPolicy", builder =>
builder.SetIsOriginAllowed((host) => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
var jwtSettings = builder.Configuration.GetSection("JwtConfigurations").Get<JwtSettings>()!;
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtConfigurations"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        ValidAudience = jwtSettings.Audience,
        ValidateAudience = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateIssuer = true,
        TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.EncryptKey)),
    };
});


builder.Services.AddAuthorization(op =>
{
    op.AddPolicy("ApplicationLogic", policy => policy
    .RequireClaim(ClaimConstants.ReadProducts.Type, ClaimConstants.ReadProducts.Value)
    .RequireClaim(ClaimConstants.CreateOrder.Type, ClaimConstants.CreateOrder.Value));

    op.AddPolicy("CanReadUserOrders", policy => policy
    .RequireClaim(ClaimConstants.VipFeature.Type, ClaimConstants.VipFeature.Value));

    op.AddPolicy("CanDeleteProduct", policy => policy
         .RequireClaim(ClaimConstants.DeleteProduct.Type, ClaimConstants.DeleteProduct.Value));

    op.AddPolicy("CanUpdateProduct", policy => policy
         .RequireClaim(ClaimConstants.ChangeProduct.Type, ClaimConstants.ChangeProduct.Value));

    op.AddPolicy("CanCreateProduct", policy => policy
         .RequireClaim(ClaimConstants.CreateProduct.Type, ClaimConstants.CreateProduct.Value));

    op.AddPolicy("CanDeleteProduct", policy => policy
         .RequireClaim(ClaimConstants.DeleteProduct.Type, ClaimConstants.DeleteProduct.Value));

    op.AddPolicy("CanReadOrders", policy => policy
         .RequireClaim(ClaimConstants.ReadOrders.Type, ClaimConstants.ReadOrders.Value));

    op.AddPolicy("FullAdminPermission", policy => policy
         .RequireClaim(ClaimConstants.CreateOrder.Type, ClaimConstants.CreateOrder.Value)
         .RequireClaim(ClaimConstants.DeleteProduct.Type, ClaimConstants.DeleteProduct.Value)
         .RequireClaim(ClaimConstants.ChangeProduct.Type, ClaimConstants.ChangeProduct.Value)
         .RequireClaim(ClaimConstants.CreateProduct.Type, ClaimConstants.CreateProduct.Value)
         .RequireClaim(ClaimConstants.ReadProducts.Type, ClaimConstants.ReadProducts.Value)
         .RequireClaim(ClaimConstants.ReadOrders.Type, ClaimConstants.ReadOrders.Value));
}



);


var app = builder.Build();
await app.SeedDataBaseAsync();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();