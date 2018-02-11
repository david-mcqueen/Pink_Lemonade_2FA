using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PinkLemonade.Core;
using PinkLemonade.Core.Models;
using ZXing.Mobile;

namespace PinkLemonade.Android.UI
{
    [Activity(Label = "List", MainLauncher = true, Icon = "@mipmap/icon")]
    public class List : Activity
    {
        List<Token> tableItems = new List<Token>();
        ListView listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            OtpManager manager = new OtpManager();

            SetContentView(Resource.Layout.CustomList);
            listView = FindViewById<ListView>(Resource.Id.List);


            Button addButton = FindViewById<Button>(Resource.Id.buttonAddToken);

            addButton.Click += async (sender, e) =>
            {
                // Initialize the scanner first so it can track the current context
                MobileBarcodeScanner.Initialize(Application);

                var scanner = new MobileBarcodeScanner();

                var result = await scanner.Scan();

                var newToken = manager.TokenScanned(result.Text);

                tableItems.Add(newToken);
            };


            tableItems.AddRange(manager.LoadTokens());

            var adpt = new ListAdapter(this, tableItems);
            listView.Adapter = adpt;

            listView.ItemClick += OnListItemClick;
            listView.ItemLongClick += OnListItemLongClick;

            TimerCallback tmCallback = (obj => 
            {
                RunOnUiThread(() => adpt.Refresh());
            });

            Timer timer = new Timer(tmCallback, "refresh", 1000, 1000);
        }

        // TODO:- This should copy to clipboard
        protected void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = tableItems[e.Position];
            Toast.MakeText(this, "Copied to Clipboard", ToastLength.Short).Show();
        }

        protected void OnListItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = tableItems[e.Position];
            tableItems.Remove(t);
            t.RemoveToken();
            Toast.MakeText(this, "Token Removed", ToastLength.Short).Show();
        }

    }
}