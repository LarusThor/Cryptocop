using System.Collections.Immutable;
using System.Text;
using Cryptocop.Repositories.Data;
using Cryptocop.Repositories.Implementations;
using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Implementations;
using Cryptocop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<CryptocopDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("CryptocopConnection") 
                           ?? "Host=localhost;Port=5432;Database=cryptocop;Username=postgres;Password=postgres";
    options.UseNpgsql(connectionString);
});

builder.Services.AddHttpClient();

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICryptoCurrencyService, CryptoCurrencyService>();
builder.Services.AddScoped<IExchangeService, ExchangeService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        //TODO: Might have to tweak according to what is in appsettings.Development.json
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var jwtTokenService = context.HttpContext.RequestServices.GetRequiredService<IJwtTokenService>();
                var tokenIdClaim = context.Principal?.Claims.FirstOrDefault(c => c.Type == "tokenId")?.Value;
                if (int.TryParse(tokenIdClaim, out var tokenId))
                {
                    var isBlacklisted = await jwtTokenService.IsTokenBlacklistedAsync(tokenId);
                    if (isBlacklisted)
                    {
                        context.Fail("Token has been blacklisted.");
                    }
                }
                else
                {
                    context.Fail("Token does not contain a valid tokenId claim.");
                }
            }
        };
    });
    
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseHttpsRedirection();


app.Run();

