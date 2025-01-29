using Microsoft.AspNetCore.Identity;

namespace Restaurant_API.Repositories.Auth
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, string role);
    }
}
