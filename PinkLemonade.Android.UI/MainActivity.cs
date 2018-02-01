using Android.App;
using Android.Widget;
using Android.OS;
using ZXing.Mobile;
using PinkLemonade.Core;

namespace PinkLemonade.Android.UI
{
    [Activity(Label = "PinkLemonade.Android.UI", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            OtpManager manager = new OtpManager();

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
            Button refreshButton = FindViewById<Button>(Resource.Id.buttonRefresh);

            TextView textLabel = FindViewById<TextView>(Resource.Id.textLabel);
            TextView textIssuer = FindViewById<TextView>(Resource.Id.textIssuer);
            TextView textSecret = FindViewById<TextView>(Resource.Id.textecret);
            TextView textTime = FindViewById<TextView>(Resource.Id.textTime);
            TextView textToken = FindViewById<TextView>(Resource.Id.textToken);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            button.Click += async (sender, e) =>
            {

                // Initialize the scanner first so it can track the current context
                MobileBarcodeScanner.Initialize(Application);

                var scanner = new MobileBarcodeScanner();

                var result = await scanner.Scan();

                var token = new Core.Models.ScannedToken(result.Text);

                var newToken = manager.TokenScanned(token);


                textLabel.Text = token.Label;
                textIssuer.Text = token.Issuer;
                textSecret.Text = token.Secret;
                textToken.Text = newToken.ComputeTotp();
                textTime.Text = newToken.RemainingSeconds().ToString();

            };


            refreshButton.Click += delegate
            {
                var token = manager.Token;

                textToken.Text = token.ComputeTotp();
                textTime.Text = token.RemainingSeconds().ToString();
            };

        }
    }
}

