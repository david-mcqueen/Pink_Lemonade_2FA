using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PinkLemonade.Core.Models;

namespace PinkLemonade.Android.UI
{
    class ListAdapter : BaseAdapter<Token>
    {
        List<Token> items;
        Activity context;

        public ListAdapter(Activity context, List<Token> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Token this[int position]
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
            view.FindViewById<TextView>(Resource.Id.Token).Text = item.TokenCode;
            view.FindViewById<TextView>(Resource.Id.Issuer).Text = item.Issuer;
            view.FindViewById<TextView>(Resource.Id.Label).Text = item.Label;

            var pb = view.FindViewById<ProgressBar>(Resource.Id.ProgressHorizontal);

            pb.SetProgress(CalculateProgress(item.RemainingSeconds), false);
            pb.ProgressDrawable.SetColorFilter(GetProgressbarColour(item.RemainingSeconds), PorterDuff.Mode.Multiply);

            return view;
        }

        private int CalculateProgress(int remainingTime)
        {
            var pct = (double)remainingTime / (double)30;

            return (int)(pct * 100);
        }

        private Color GetProgressbarColour(int progress)
        {
            if (progress < 5)
                return Color.OrangeRed;

            if (progress < 10)
                return Color.DarkOrange;

            if (progress < 15)
                return Color.YellowGreen;

            else
                return Color.LawnGreen;
            
        }


        public void Refresh()
        {
            NotifyDataSetChanged();
        }
    }

}