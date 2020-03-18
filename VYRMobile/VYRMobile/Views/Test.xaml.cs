using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using VYRMobile.Helper;
using VYRMobile.ViewModels;
using Plugin.FilePicker;

namespace VYRMobile.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile]
    public partial class Test : ContentPage
    {
        bool IsUrgent = false;
        public Test()
        {
            InitializeComponent();
            BindingContext = new CallViewModel();
            alarmMode();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void alarmMode()
        {
            await Task.Delay(2500);
            IsUrgent = true;
            while(IsUrgent)
            {
                /*await Task.Delay(50);
                lateral1.Opacity = 0.9;
                lateral2.Opacity = 0.9;
                await Task.Delay(50);
                lateral1.Opacity = 0.8;
                lateral2.Opacity = 0.8;
                await Task.Delay(50);
                lateral1.Opacity = 0.7;
                lateral2.Opacity = 0.7;
                await Task.Delay(50);
                lateral1.Opacity = 0.6;
                lateral2.Opacity = 0.6;
                await Task.Delay(50);
                lateral1.Opacity = 0.5;
                lateral2.Opacity = 0.5;
                await Task.Delay(50);
                lateral1.Opacity = 0.6;
                lateral2.Opacity = 0.6;
                await Task.Delay(50);
                lateral1.Opacity = 0.7;
                lateral2.Opacity = 0.7;
                await Task.Delay(50);
                lateral1.Opacity = 0.8;
                lateral2.Opacity = 0.8;
                await Task.Delay(50);
                lateral1.Opacity = 0.8;
                lateral2.Opacity = 0.8;*/


                await animation.FadeTo(0.5, 500, Easing.Linear);
                //await lateral2.FadeTo(0.5, 300, Easing.Linear);
                await animation.FadeTo(1, 500, Easing.Linear);
                //await lateral2.FadeTo(1, 300, Easing.Linear);
            }
        }
        /*private async void btnPick_Clicked(object sender, EventArgs e)
        {
            try
            {
                var fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return;

                string fileName = fileData.FileName;
                byte[] contents = fileData.DataArray;

                var Uri = await _azureHelper.SaveBlockBlob("images", contents, fileName);
                Link.Text = Uri;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }*/
        /*private void PubNub()
        {
            PNConfiguration pnConfiguration = new PNConfiguration();
            pnConfiguration.PublishKey = "pub-c-78ea4244-4ba9-460d-acea-39d7f1c51a52";
            pnConfiguration.SubscribeKey = "sub-c-9f8d4e36-5a53-11ea-b226-5aef0d0da10f";

            Pubnub pubnub = new Pubnub(pnConfiguration);

            SubscribeCallbackExt mySubscribeListener = new SubscribeCallbackExt(
            delegate (Pubnub pnObj, PNMessageResult<object> message)
            {
                Console.WriteLine(pubnub.JsonPluggableLibrary.SerializeToJsonString(message));
            },
            delegate (Pubnub pnObj, PNPresenceEventResult presence)
            {
                if (presence != null)
                {
                    Console.WriteLine(pubnub.JsonPluggableLibrary.SerializeToJsonString(presence));
                }
            },
            delegate (Pubnub pnObj, PNStatus status)
            {
                if (status != null && status.StatusCode == 200 && status.Category == PNStatusCategory.PNConnectedCategory)
                {
                    pubnub.Publish()
                    .Channel("pubnub_onboarding_channel")
                    .Message(new Dictionary<string, string>() { { "sender", pnConfiguration.Uuid }, { "content", "Hello From C# SDK" } })
                    .Execute(new PNPublishResultExt((result, publishStatus) =>
                    {
                        if (result != null)
                        {
                            Console.WriteLine(pubnub.JsonPluggableLibrary.SerializeToJsonString(result));
                        }
                        else if (publishStatus != null && publishStatus.Error && publishStatus.ErrorData != null)
                        {
                            Console.WriteLine(pubnub.JsonPluggableLibrary.SerializeToJsonString(publishStatus));
                        }
                    }));

                }
                if (status.Error && status.ErrorData != null)
                {
                    Console.WriteLine(pubnub.JsonPluggableLibrary.SerializeToJsonString(status.ErrorData.Information));
                }
            }
            );

            pubnub.AddListener(mySubscribeListener);

            pubnub.Subscribe<string>()
            .Channels(new string[] { "pubnub_onboarding_channel" })
            .WithPresence()
            .Execute();

            pubnub.History()
            .Channel("pubnub_onboarding_channel")
            .Execute(new PNHistoryResultExt((result, status) =>
            {
                Console.WriteLine(pubnub.JsonPluggableLibrary.SerializeToJsonString(result));
            }));

            Console.ReadLine();
        }*/
    }
}