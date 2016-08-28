using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using MyDebts.Entities;

namespace MyDebts.Logic
{
    class DebtListViewAdapter : BaseAdapter<Person>
    {
        private List<Person> _data;
        private Context _context;
        private Activity _activity;

        public DebtListViewAdapter(Activity activity, Context context, List<Person> data)
        {
            _data = data;
            _context = context;
            _activity = activity;
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
                row = LayoutInflater.From(_context).Inflate(Resource.Layout.DebtListViewRow, null, false);

            row.FindViewById<TextView>(Resource.Id.RowName).Text = _data[position].Name;
            row.FindViewById<TextView>(Resource.Id.RowAmount).Text = _data[position].Amount.ToString("0.00");

            if (String.IsNullOrEmpty(_data[position].Comment))
                row.FindViewById<TextView>(Resource.Id.RowComment).Visibility = ViewStates.Gone;
            else
                row.FindViewById<TextView>(Resource.Id.RowComment).Text = _data[position].Comment;

            row.FindViewById<ImageView>(Resource.Id.RowDebtIcon).SetImageResource
                (_data[position].MyDebt ? Resource.Drawable.IcoBad : Resource.Drawable.IcoGood);

            var editBtn = row.FindViewById<ImageButton>(Resource.Id.RowEdit);
            editBtn.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(_context, typeof(Activities.EditActivity));
                intent.PutExtra("id", _data[position].Id);
                _context.StartActivity(intent);
            };

            return row;
        }

        
    }
}