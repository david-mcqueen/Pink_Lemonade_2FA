using Android.App;
using Android.Widget;
using Android.OS;
using ZXing.Mobile;
using PinkLemonade.Core;
using Android.Content;
using System;
using PinkLemonade.DataAccess.Entities;
using PinkLemonade.DataAccess;

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
            Button addButton = FindViewById<Button>(Resource.Id.buttonAddToken);
            Button viewTokensButton = FindViewById<Button>(Resource.Id.buttonViewTokens);

            viewTokensButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(List));
                StartActivity(intent);
            };

            addButton.Click += async (sender, e) =>
            {
                // Initialize the scanner first so it can track the current context
                MobileBarcodeScanner.Initialize(Application);

                var scanner = new MobileBarcodeScanner();

                var result = await scanner.Scan();

                manager.TokenScanned(result.Text);
            };
        }
    }
}

