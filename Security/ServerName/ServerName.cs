using Microsoft.AspNetCore.Http;

namespace API.Security.ServerName
{
    public class ServerName : IServerName
    {
        private readonly IHttpContextAccessor _httpAccessor;

        public ServerName(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;
        }
        public string GetServerName()
        {
            bool isSecure = _httpAccessor.HttpContext.Request.IsHttps;
            string serverName = _httpAccessor.HttpContext.Request.Host.Value;
            if(isSecure) 
                return "https://"+serverName;
            return "http://"+serverName;
        }
    }
}