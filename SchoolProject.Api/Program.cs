using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolProject.Core;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrustructure;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

#region connect to database
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));
#endregion

#region depency injection
builder.Services.AddInfrustructureDependencies()
    .AddServiceDependencies()
    .AddCoreDependencies();

#region registerservice

#region identityservices
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;

}).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();
#endregion

#region jwt authentication setting to apply authenticaation on project
var JwtSetting = new JwtSetting();
builder.Configuration.GetSection("jwtSettings").Bind(JwtSetting); // making bind to transfer data from jwtsetting in appsetting.json into class jwtsetting
builder.Services.AddSingleton(JwtSetting);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = JwtSetting.ValidateIssuer,
        ValidIssuers = new[] { JwtSetting.Issuer },
        ValidateIssuerSigningKey = JwtSetting.ValidateIssuerSigningKey,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSetting.Secret)),
        ValidAudience = JwtSetting.Audience,
        ValidateAudience = JwtSetting.ValidateAudience,
        ValidateLifetime = JwtSetting.ValidateLifeTime,
    };
});
#endregion

#region Swagger generator auth
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Project", Version = "v1" });

    c.EnableAnnotations();

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
            }
           });
});
#endregion

#endregion

#endregion

#region Addlocalization
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "";
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("de-DE"),
        new CultureInfo("fr-FR"),
        new CultureInfo("en-GB"),
        new CultureInfo("ar-EG"),
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
#endregion

#region Addcors
var Cors = "_cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors,
    builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
});
#endregion



// start middleware
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// for handle error
//app.UseMiddleware<ErrorHandlerMiddleware>();

#region middleware localization
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion

app.UseCors(Cors);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();