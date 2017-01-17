using Android.App;
using Android.Content.PM;
using Android.Net.Wifi;
using Android.OS;

namespace JunAndChihiro.Droid
{
    [Activity(Label = "J & C", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

            WifiManager wifiManager = (WifiManager)GetSystemService(WifiService);
            if (wifiManager.ConnectionInfo.SSID == "\"BELL221\"")
                Constants.SetLocalIp(true);

            var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            var y = typeof(Xamarin.Forms.Themes.LightThemeResources);
            var z = typeof(Xamarin.Forms.Themes.Android.UnderlineEffect);
        }
    }
}

