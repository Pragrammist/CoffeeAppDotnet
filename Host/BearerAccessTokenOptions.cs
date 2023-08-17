using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

class CorsOptions
{
    public string[] AllowerOrigins { get; set; } = null!;
}


public class BearerAccessTokenOptions
{
    public string GetBearerToken(List<Claim> claims) => new JwtSecurityTokenHandler()
        .WriteToken(
            JwtSecurityToken(claims)
         );


    public JwtSecurityToken JwtSecurityToken(List<Claim> claims) => new JwtSecurityToken(
        issuer: Issuer,
        audience: Audience,
        notBefore: DateTime.UtcNow,
        claims: claims,
        expires: BearerTokenLifeTime,
        signingCredentials: SigningCredentials
    );
    public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256;
    public SigningCredentials SigningCredentials => new SigningCredentials(GetSymmetricSecurityKey, SecurityAlgorithm);
    public DateTime BearerTokenLifeTime => DateTime.UtcNow.Add(TimeSpan.FromHours(Lifetime));
    public bool RequiredHttpsMetadata { get; set; }
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Key { get; set; } = null!;
    public int Lifetime { get; set; } 
    public SymmetricSecurityKey GetSymmetricSecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));

    public TokenValidationParameters TokenValidationParameters => new()
    {
        // укзывает, будет ли валидироваться издатель при валидации токена
        ValidateIssuer = true,
        // строка, представляющая издателя
        ValidIssuer = Issuer,

        // будет ли валидироваться потребитель токена
        ValidateAudience = true,
        // установка потребителя токена
        ValidAudience = Audience,
        // будет ли валидироваться время существования
        ValidateLifetime = true,

        ClockSkew = TimeSpan.Zero,
        // установка ключа безопасности
        IssuerSigningKey = GetSymmetricSecurityKey,
        // валидация ключа безопасности
        ValidateIssuerSigningKey = true,
    };
}