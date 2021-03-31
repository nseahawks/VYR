using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VYRMobile.Models;

namespace VYRMobile.ViewModels
{
    public class NotificationViewModel : BaseViewModel
    {
        private ObservableCollection<NotificationItem> notifications;
        public ObservableCollection<NotificationItem> Notifications
        {
            get { return notifications; }
            set
            {
                notifications = value;
                OnPropertyChanged();
            }
        }
        public NotificationViewModel()
        {
            Notifications = new ObservableCollection<NotificationItem>();

            LoadItems();
        }
        private void LoadItems()
        {
            NotificationItem one = new NotificationItem()
            {
                Date = DateTime.Now,
                Icon = "msg.png",
                IsNew = true,
                Name = "Mensaje",
                Owner = "Monitoreo",
                Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat."
            };
            NotificationItem two = new NotificationItem()
            {
                Date = DateTime.Now,
                Icon = "msg.png",
                IsNew = true,
                Name = "Mensaje",
                Owner = "Monitoreo",
                Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat."
            };
            NotificationItem three = new NotificationItem()
            {
                Date = DateTime.Now,
                Icon = "msg.png",
                IsNew = true,
                Name = "Mensaje",
                Owner = "Monitoreo",
                Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat."
            };
            NotificationItem four = new NotificationItem()
            {
                Date = DateTime.Now,
                Icon = "msg.png",
                IsNew = false,
                Name = "Mensaje",
                Owner = "Monitoreo",
                Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat."
            };

            NotificationItem five = new NotificationItem()
            {
                Date = DateTime.Now,
                Icon = "msg.png",
                IsNew = false,
                Name = "Mensaje",
                Owner = "Monitoreo",
                Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat."
            };

            Notifications.Add(one);
            Notifications.Add(two);
            Notifications.Add(three);
            Notifications.Add(four);
            Notifications.Add(five);
        }
    }
}
