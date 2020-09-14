using Android.Content;
using Android.Locations;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace VYRMobile.Helper
{
    public class PermissionsHelper
    {
        public async Task<bool> CheckLocationPermissionsStatus()
        {
            var permissionStatus = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            var locManager = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);
            bool isGPSEnabled = locManager.IsProviderEnabled(LocationManager.GpsProvider);

            if (permissionStatus != PermissionStatus.Granted)
            {
                permissionStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (permissionStatus != PermissionStatus.Granted)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (!isGPSEnabled)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<bool> CheckCameraPermissionsStatus()
        {
            var permissionStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (permissionStatus != PermissionStatus.Granted)
            {
                permissionStatus = await Permissions.RequestAsync<Permissions.Camera>();

                if (permissionStatus != PermissionStatus.Granted)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        public async Task<bool> CheckStoragePermissionsStatus()
        {
            var writeStoragePermissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            var readStoragePermissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (writeStoragePermissionStatus != PermissionStatus.Granted)
            {
                writeStoragePermissionStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();

                if (writeStoragePermissionStatus != PermissionStatus.Granted)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (readStoragePermissionStatus != PermissionStatus.Granted)
            {
                readStoragePermissionStatus = await Permissions.RequestAsync<Permissions.StorageRead>();

                if (readStoragePermissionStatus != PermissionStatus.Granted)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        public async Task<bool> CheckPhonecallPermissionsStatus()
        {
            var permissionStatus = await Permissions.CheckStatusAsync<Permissions.Phone>();

            if (permissionStatus != PermissionStatus.Granted)
            {
                permissionStatus = await Permissions.RequestAsync<Permissions.Phone>();

                if (permissionStatus != PermissionStatus.Granted)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
