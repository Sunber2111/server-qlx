using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace API.Security.UserAccessor
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpAccessor;

        public UserAccessor(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;
        }

        public string GetCurrentUserId()
        {
            var username = _httpAccessor.HttpContext.User?.Claims?.FirstOrDefault(t => t.Type == ClaimTypes.NameIdentifier)?.Value;
            return username;
        }
    }
}