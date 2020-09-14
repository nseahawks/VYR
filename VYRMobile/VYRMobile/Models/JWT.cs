using MvvmHelpers;

namespace VYRMobile.Models
{
    class JWT : ObservableObject
    {
        string token;
        public string Token
        {
            get => token;
            set => SetProperty(ref token, value);
        }
    }
}
