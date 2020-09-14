using System;

namespace VYRMobile.Services
{
    public interface IToast
    {
        void LongToast(string message);
        void ShortToast(string message);
    }
}
