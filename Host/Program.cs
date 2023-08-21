using Services;
using EFCore;
using Host.Middlewares;
using Services.Contracts;
using Services.Dtos.User;
using Domain.Exceptions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = "bearer_access_token";
    x.DefaultChallengeScheme = "bearer_access_token";
    x.DefaultScheme = "bearer_access_token";
})
.AddJwtBearer("bearer_access_token", options =>
{
    var bearerOptions = builder.Configuration.GetSection("Auth:Bearer").Get<BearerAccessTokenOptions>() ?? throw new NullReferenceException("Bearer options is null");
    options.RequireHttpsMetadata = bearerOptions.RequiredHttpsMetadata;
    options.TokenValidationParameters = bearerOptions.TokenValidationParameters;
});

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.Configure<BearerAccessTokenOptions>(builder.Configuration.GetSection("Auth:Bearer"));
builder.Services.AddAuthorization();

builder.Services.RegisterDataLayer(builder.Configuration);
builder.Services.AddAppServices(builder.Configuration);



var app = builder.Build();

//init first users
{
    using var scope = app.Services.CreateScope();
    var userService = scope.ServiceProvider.GetService<IUserService>() ?? throw new NullReferenceException("IUserService is null when init db");
    var firstUsers = builder.Configuration.GetSection("FirstUsers").Get<CreateUserDto[]>() ?? throw new NullReferenceException("First users are null");
    foreach (var user in firstUsers)
    {
        try
        {
            await userService.CreateUser(user);
        }
        catch (UserDataNotValidException)
        {
            continue;
        }
        
    }
}


// Configure the HTTP request pipeline.

var corsOptions = builder.Configuration.GetSection("Cors").Get<CorsOptions>() ?? throw new NullReferenceException("Cors options is null");
app.UseMiddleware<HandlingCoffeeApplicationExceptionsMiddleware>();
app.UseCors(builder => builder.WithOrigins(corsOptions.AllowerOrigins).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

app.UseRouting();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();


app.Run();