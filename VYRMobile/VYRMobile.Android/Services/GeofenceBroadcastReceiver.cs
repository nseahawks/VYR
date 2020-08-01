﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Location;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace VYRMobile.Droid.Services
{
    [BroadcastReceiver()]
    public class GeofenceBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            GeofencingEvent geofencingEvent = GeofencingEvent.FromIntent(intent);
            if (geofencingEvent.HasError)
            {
                string error = GeofenceStatusCodes.GetStatusCodeString(geofencingEvent.ErrorCode);
                Console.WriteLine($"Error Code: {geofencingEvent.ErrorCode}. Error: {error}");
                return;
            }

            // Get the transition type.
            int geofenceTransition = geofencingEvent.GeofenceTransition;
            // Get the geofences that were triggered. A single event can trigger
            // multiple geofences.
            //IList<IGeofence> triggeringGeofences = geofencingEvent.TriggeringGeofences;
            //NotificationsService notificationsService = new NotificationsService(context);

            switch (geofenceTransition)
            {
                case Geofence.GeofenceTransitionEnter:
                    //SendEmail("Vigilante acaba de entrar al area asignada en la fecha y hora: " + DateTime.Now.ToString());
                    break;
                case Geofence.GeofenceTransitionExit:
                    //SendEmail("Vigilante acaba de salir del area asignada en la fecha y hora: " + DateTime.Now.ToString());
                    break;
                default:
                    // Log the error.
                    Console.WriteLine("Broadcast not implemented.");
                    break;
            }
        }
        private async Task SendEmail(string body)
        {
            string destinator = "ciberash04@gmail.com";
            List<string> recipients = new List<string>();

            recipients.Add(destinator);
            var message = new EmailMessage
            {
                Subject = "Aviso de movimiento",
                Body = body,
                To = recipients
            };
            await Email.ComposeAsync(message);
        }
    }
}