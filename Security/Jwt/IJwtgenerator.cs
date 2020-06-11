using API.Models;

namespace API.Security.Jwt
{
    public interface IJwtgenerator
    {
        string CreateToken(NhanVien nv);
    }
}