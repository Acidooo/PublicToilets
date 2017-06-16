using System;
using System.IO;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using DownloadExample.Droid;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Droid.Platform;
using MvvmCross.Forms.Droid.Presenters;
using MvvmCross.Platform;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using Xamarin.Forms;

namespace PublicPoo.Droid
{
    [Activity(Label = "PublicToilets", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Context ApplicationContext { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            try
            {
                //MK Possilbe fix for an issue where Mvx.Resolve<> throws object null reference
                //   https://github.com/MvvmCross/MvvmCross/issues/1192
                //   https://github.com/MvvmCross/MvvmCross/issues/1245
                var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
                setup.EnsureInitialized();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            base.OnCreate(bundle);
            
            Forms.Init(this, bundle);

            // Set up mvvmcross
            var mvxFormsApp = new App();
            LoadApplication(mvxFormsApp);

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsDroidPagePresenter;
            if (presenter != null) presenter.MvxFormsApp = mvxFormsApp;

            Mvx.Resolve<IMvxAppStart>().Start();

            ApplicationContext = this;
            InitDownloadManager();
        }

        NotificationClickedBroadcastReceiver _receiverNotificationClicked;

        protected override void OnResume()
        {
            base.OnResume();

            _receiverNotificationClicked = new NotificationClickedBroadcastReceiver();
            RegisterReceiver(
                _receiverNotificationClicked,
                new IntentFilter(DownloadManager.ActionNotificationClicked)
            );
        }

        protected override void OnPause()
        {
            base.OnPause();

            UnregisterReceiver(_receiverNotificationClicked);
        }

        void InitDownloadManager()
        {
            // Define where the files should be stored. MUST be an external storage. (see https://github.com/SimonSimCity/Xamarin-CrossDownloadManager/issues/10)
            // If you skip this, you neither need the permission `WRITE_EXTERNAL_STORAGE`.
            CrossDownloadManager.Current.PathNameForDownloadedFile = new Func<IDownloadFile, string>(file =>
            {
                string fileName = Android.Net.Uri.Parse(file.Url).Path.Split('/').Last();
                return Path.Combine(ApplicationContext.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath, fileName);
            });
        }
    }
}

