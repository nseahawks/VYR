using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using VYRMobile.Views.Popups;
using Xamarin.Essentials;

namespace VYRMobile.Helper
{
    public class CognitoIdentityHelper
    {
        private static AmazonCognitoIdentityProviderClient cognitoIdentityProviderClient;
        public static AmazonCognitoIdentityProviderClient CognitoIdentityProviderClient
        {
            get
            {
                if (cognitoIdentityProviderClient == null)
                {
                    cognitoIdentityProviderClient = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint.USEast2);
                }

                return cognitoIdentityProviderClient;
            }
        }
        private static CognitoUserPool cognitoUserPool;
        public static CognitoUserPool CognitoUserPool
        {
            get
            {
                if (cognitoUserPool == null)
                {
                    cognitoUserPool = new CognitoUserPool(Constants.CognitoUserPoolId, Constants.CognitoClientId, CognitoIdentityProviderClient);
                }

                return cognitoUserPool;
            }
        }
        public static string Token
        {
            get => Preferences.Get(nameof(Token), "");
            set => Preferences.Set(nameof(Token), value);
        }
        public CognitoIdentityHelper()
        {
            
        }
        public async Task<bool> LoginAuth(string username, string password)
        {
            CognitoUser User = new CognitoUser(username, Constants.CognitoClientId, CognitoUserPool, CognitoIdentityProviderClient, Constants.CognitoClientSecretHash);

            InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
            {
                Password = password
            };

            AuthFlowResponse response = await User.StartWithSrpAuthAsync(authRequest);

            while (response.AuthenticationResult == null)
            {
                if (response.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
                {
                    await App.Current.MainPage.Navigation.PushPopupAsync(new NewPasswordPopup());

                    while(App.CognitoIdentityNewPassword == "")
                    {
                        await Task.Delay(100);
                    }

                    await App.Current.MainPage.Navigation.PushPopupAsync(new LoadingPopup());

                    response = await User.RespondToNewPasswordRequiredAsync(new RespondToNewPasswordRequiredRequest()
                    {
                        SessionID = response.SessionID,
                        NewPassword = App.CognitoIdentityNewPassword
                    });
                    Token = response.AuthenticationResult.AccessToken;

                    await App.Current.MainPage.Navigation.PopPopupAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Fallido", "Metodo de autenticación no reconocido", "Aceptar");
                    Token = "";
                    return false;
                }
            }

            if (response.AuthenticationResult != null)
            {
                App.IsUserLoggedIn = true;
                Token = response.AuthenticationResult.AccessToken;
                ApiHelper.Token = Token;
                DeserializeToken(Token);
                return true;
            }
            else
            {
                App.IsUserLoggedIn = false;
                return false;
            }
        }
        public async void DeserializeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            App.ApplicationUserToken = handler.ReadToken(token) as JwtSecurityToken;

            Dictionary<string, object> dictionary = App.ApplicationUserToken.Payload;

            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                await SecureStorage.SetAsync(pair.Key.ToString(), pair.Value.ToString());
            }

            await SecureStorage.SetAsync("isLogged", App.IsUserLoggedIn.ToString());

            App.ApplicationUserId = await SecureStorage.GetAsync("sub");
        }
    }
}
