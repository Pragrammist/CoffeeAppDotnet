using Host.Infrastructure.Consts;

namespace Host.Infrastructure.Services
{
    public class CurrentUserService
    {
        readonly IHttpContextAccessor _httpContext;
        public CurrentUserService(IHttpContextAccessor httpContext) 
        {
            _httpContext = httpContext;
        }

        public int GetCurrentIdUser()
        {
            var claimValueId = _httpContext.HttpContext?.User.FindFirst(f => f.Type == ClaimsConst.ID) ?? throw new NullReferenceException("Current user id is null");

            var userId = int.Parse(claimValueId.Value);
            
            return userId;
        }
    }
}
