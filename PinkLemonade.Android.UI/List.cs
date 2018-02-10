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
using PinkLemonade.Core;
using PinkLemonade.Core.Models;

namespace PinkLemonade.Android.UI
{
    [Activity(Label = "List")]
    public class List : Activity
    {
        List<Token> tableItems = new List<Token>();
        ListView listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.CustomList);
            listView = FindViewById<ListView>(Resource.Id.List);

            OtpManager manager = new OtpManager();
            tableItems.AddRange(manager.LoadTokens());

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