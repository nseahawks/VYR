/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.PushNotification;

[Application]
public class MainApplication : Application
{
    public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
    {
        base.OnCreate();

        //Set the default notification channel for your app when running Android Oreo
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            /*
             * For a single Notification channel
             */
            //Change for your default notification channel id here
            /*PushNotificationManager.DefaultNotificationChannelId = "DefaultChannel";

            //Change for your default notification channel name here
            /*PushNotificationManager.DefaultNotificationChannelName = "General";


            /*
             * Or to work with multiple notification channels
             * e.g. to enable multiple importance level messages or different notification sounds...etc
             * Note: Once NotificationChannels contains at least one element, DefaultNotificationChannelId, DefaultNotificationChannelName, 
             * and DefaultNotificationChannelImportanceLevel are ignored.
             */
            /*PushNotificationManager.NotificationChannels = new List<NotificationChannelProps>()
                {
                    new NotificationChannelProps("infoMessagesId", "Informations"),
                    new NotificationChannelProps("warningMessagesId", "Warnings", NotificationImportance.High),
                    new NotificationChannelProps("reminderMessagesId", "Reminders", NotificationImportance.Min)
                };

        }


        //If debug you should reset the token each time.
        /*#if DEBUG
        PushNotificationManager.Initialize(this, true);
        #else
              PushNotificationManager.Initialize(this,false);
        #endif

        //Handle notification when app is closed here
        /*CrossPushNotification.Current.OnNotificationReceived += (s, p) =>
        {


        };
    }
}*/