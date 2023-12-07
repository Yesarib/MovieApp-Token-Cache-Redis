using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MovieApp.Cache;
using MovieApp.Core.Configuration;
using MovieApp.Core.Models;
using MovieApp.Core.Repositories;
using MovieApp.Core.Services;
using MovieApp.Core.UnitOfWork;
using MovieApp.Repository;
using MovieApp.Repository.Repositories;
using MovieApp.Repository.UnitOfWork;
using MovieApp.Service;
using MovieApp.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOptions"));
builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddScoped<IMovieRepositoryForRedis>(sp =>
{
    var appDbContextForRedis = sp.GetRequiredService<AppDbContext>();
    var movieRepository = new MovieRepositoryForRedis(appDbContextForRedis);
    var redisService = sp.GetRequiredService<RedisService>();

    return new MovieRepositoryWithRedis(movieRepository, redisService);
});
builder.Services.AddScoped<IMovieServiceForRedis, MovieServiceForRedis>();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IServiceGeneric<,>),typeof(GenericService<,>));
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<CacheService>();
builder.Services.AddMemoryCache();

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOptions>();
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("MovieApp.Repository");
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("myDatabase"); 
});


builder.Services.AddSingleton<RedisService>(sp =>
{
    return new RedisService(builder.Configuration["CacheOptions:Url"]);
});

// Always calling redis
builder.Services.AddSingleton(sp =>
{
    var redisService = sp.GetRequiredService<RedisService>();
    return redisService.GetDb(0);
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
