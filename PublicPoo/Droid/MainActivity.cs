using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Droid.Platform;
using MvvmCross.Forms.Presenter.Droid;
using MvvmCross.Platform;
using Xamarin.Forms;

namespace PublicPoo.Droid
{
    [Activity(Label = "PublicToilets", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
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
        }
    }
}

