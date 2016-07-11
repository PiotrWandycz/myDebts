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
using MyDebts.Entities;

namespace MyDebts.Resources
{
    class DebtListViewAdapter : BaseAdapter<Person>
    {
        private List<Person> _data;
        private Context _context;

        public DebtListViewAdapter(Context context, List<Person> data)
        {
            _data = data;
            _context = context;
        }

        public override Person this[int position]
        {
            get { return _data[position]; }
        }

        public override int Count
        {
            get { return _data.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(_context).Inflate(Resource.Layout.DebtListViewRow, null, false);
            }

            row.FindViewById<TextView>(Resource.Id.Name).Text = _data[position].Name;
            row.FindViewById<TextView>(Resource.Id.Amount).Text = _data[position].Amount.ToString("0.00");
            row.FindViewById<TextView>(Resource.Id.When).Text = _data[position].When.ToString("dd.MM.yy");

            if (String.IsNullOrEmpty(_data[position].Comment))
                row.FindViewById<TextView>(Resource.Id.Comment).Visibility = ViewStates.Gone;
            else
                row.FindViewById<TextView>(Resource.Id.Comment).Text = _data[position].Comment;

            row.FindViewById<LinearLayout>(Resource.Id.Control).SetBackgroundColor
                (_data[position].OwesMe ? Android.Graphics.Color.DarkGreen : Android.Graphics.Color.DarkRed);
           
            return row;
        }
    }
}