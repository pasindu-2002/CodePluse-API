using Microsoft.AspNetCore.Identity;

namespace CodePluse.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
