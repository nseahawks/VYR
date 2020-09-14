using System.Threading.Tasks;

namespace VYRMobile.Services
{
    interface IChangePassword<T>
    {
        Task<bool> ChangePasswordAsync(T password);
    }
}
