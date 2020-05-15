using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;

namespace VYRMobile.Services
{
    interface IChangePassword<T>
    {
        Task<bool> ChangePasswordAsync(T password);
    }
}
