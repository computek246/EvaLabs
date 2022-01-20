using System.Security.Principal;

namespace EvaLabs.Interfaces
{
    public interface ICurrentUserService<out TKey>
    {
        TKey UserId { get; }
        IPrincipal Principal { get; }
        bool IsAuthenticated { get; }
    }
}