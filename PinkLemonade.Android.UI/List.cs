using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PinkLemonade.Core.Models;

namespace PinkLemonade.Android.UI
{
    [Activity(Label = "List")]
    public class List : Activity
    {
        List<ScannedToken> tableItems = new List<ScannedToken>();
        ListView listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CustomList);
            listView = FindViewById<ListView>(Resource.Id.List);

            tableItems.Add(new ScannedToken("otpauth://totp/ACME%20Co:john@example.com?secret=HXDMVJECJJWSRB3HWIZR4IFUGFTMXBOZ&issuer=ACME%20Co&algorithm=SHA1&digits=6&period=30"));
            tableItems.Add(new ScannedToken("otpauth://totp/ACME%20Co:john@example.com?secret=HXDMVJECJJWSRB3HWIZR4IFUGFTMXBOZ&issuer=ACME%20Co&algorithm=SHA1&digits=6&period=30"));

            listView.Adapter = new ListAdapter(this, tableItems);

            listView.ItemClick += OnListItemClick;
        }

        protected void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = tableItems[e.Position];
            Toast.MakeText(this, "CATS", ToastLength.Short).Show();
            Console.WriteLine("Clicked on " + "CATS");
        }
    }
}