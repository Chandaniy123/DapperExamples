
using CQRsAndMEdiatorsEXample.Repository;
using CQRsAndMEdiatorsEXample.Service;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IQuizRepo, QuizRepo>();
builder.Services.AddScoped<IPartcipentRepo,PartcipentRepo>();
builder.Services.AddScoped<ITokenGenerateService,TokenGenerateService>();

ValiDateTokenParameter(builder.Services, builder.Configuration);
void ValiDateTokenParameter(IServiceCollection services, ConfigurationManager configuration)
{
    var userSecretKey = configuration["JwtValidationDetails:UserAppSecretKey"];
    var userIssuer = configuration["JwtValidationDetails:UserIssuer"];
    var userAudience = configuration["JwtValidationDetails:UserAudience"];

    var userSecrteKey = Encoding.UTF8.GetBytes(userSecretKey);
    var userSymmetricSecurityKey = new SymmetricSecurityKey(userSecrteKey);
    var tokenValidParameter = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = userIssuer,

        ValidateAudience = true,
        ValidAudience = userAudience,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = userSymmetricSecurityKey,
    };


    services.AddAuthentication(u =>
    {
        u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(u => u.TokenValidationParameters = tokenValidParameter);
}
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
