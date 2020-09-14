using System.Threading.Tasks;

namespace VYRMobile.Services
{
    public interface IIdentityService<T>
    {
        Task<bool> LoginAsync(T user);
        Task<bool> RefreshTokenAsync(string token, string refreshToken);
    }
}
