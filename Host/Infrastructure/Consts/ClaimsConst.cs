using System.Security.Claims;

namespace Host.Infrastructure.Consts
{
    public static class ClaimsConst
    {
        public const string LOGIN = ClaimsIdentity.DefaultNameClaimType;
        public const string ROLE = ClaimsIdentity.DefaultRoleClaimType;
        public const string EMAIL = "email";
        public const string ID = "id";

    }
}
