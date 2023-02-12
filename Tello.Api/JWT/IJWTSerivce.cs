using NuGet.Protocol.Plugins;
using Tello.Core.Entities;

namespace Tello.Api.JWT
{
    public interface IJWTSerivce
    {
        public string CreateJWTToken(AppUser user, IList<string> roles);
    }
}
