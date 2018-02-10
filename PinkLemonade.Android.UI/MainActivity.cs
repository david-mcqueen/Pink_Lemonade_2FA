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
            Button refreshButton = FindViewById<Button>(Resource.Id.buttonRefresh);
            Button viewTokensButton = FindViewById<Button>(Resource.Id.buttonViewTokens);

            TextView textLabel = FindViewById<TextView>(Resource.Id.textLabel);
            TextView textIssuer = FindViewById<TextView>(Resource.Id.textIssuer);
            TextView textSecret = FindViewById<TextView>(Resource.Id.textecret);
            TextView textTime = FindViewById<TextView>(Resource.Id.textTime);
            TextView textToken = FindViewById<TextView>(Resource.Id.textToken);

            viewTokensButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(List));
                //intent.PutExtra("token_manager", manager);
                //intent.PutStringArrayListExtra("countries", countries);
                StartActivity(intent);
            };


            addButton.Click += async (sender, e) =>
            {

                // Initialize the scanner first so it can track the current context
                MobileBarcodeScanner.Initialize(Application);

                var scanner = new MobileBarcodeScanner();

                var result = await scanner.Scan();

                var newToken = manager.TokenScanned(result.Text);

                textLabel.Text = newToken.Label;
                textIssuer.Text = newToken.Issuer;
                textSecret.Text = newToken.Secret;
                textToken.Text = newToken.TokenCode;
                textTime.Text = newToken.RemainingSecondsString;



            };


            refreshButton.Click += delegate
            {
                manager.LoadTokens();
                var token = manager.GetFirstToken();

                if (token == null)
                    return;

                textToken.Text = token.TokenCode;
                textTime.Text = token.RemainingSecondsString;
            };

        }
    }
}

