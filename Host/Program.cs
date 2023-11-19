using Services;
using EFCore;
using Host.Middlewares;
using Services.Contracts;
using Services.Dtos.User;
using Domain.Exceptions;
using Host.Infrastructure.Mapping.Coffee;
using Domain.Options;

var builder = WebApplication.CreateBuilder(args);

CoffeeMappingSettings.SetCoffeeMapping();
CommentMappingSettings.SetCommentMapping();

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

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<FileStoringOptions>(builder.Configuration.GetSection("Files"));
builder.Services.Configure<BearerAccessTokenOptions>(builder.Configuration.GetSection("Auth:Bearer"));

builder.Services.AddAuthorization();

builder.Services.RegisterDataLayer(builder.Configuration);
builder.Services.AddAppServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);
});
builder.Services.AddSwaggerGen();




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
app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseCors(builder => builder.WithOrigins(corsOptions.AllowerOrigins).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

app.UseRouting();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();


app.Run();
