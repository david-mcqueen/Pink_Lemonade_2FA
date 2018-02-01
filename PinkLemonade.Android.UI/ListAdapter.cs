﻿using System;
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
    class ListAdapter : BaseAdapter<ScannedToken>
    {
        List<ScannedToken> items;
        Activity context;

        public ListAdapter(Activity context, List<ScannedToken> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override ScannedToken this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }
        
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomListCell, null);
            view.FindViewById<TextView>(Resource.Id.Token).Text = item.Issuer;
            view.FindViewById<TextView>(Resource.Id.Timeout).Text = item.Label;

            return view;
        }
    }

}