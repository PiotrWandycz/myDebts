using System.Linq;
using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.OS;
using MyDebts.Entities;
using MyDebts.DB.DataServices;
using Android.Views;
using Android.Content;
using MyDebts.Logic;

namespace MyDebts.Activities
{
    [Activity(MainLauncher = true, Icon = "@drawable/appIcon")]
    public class MainActivity : Activity
    {
        PersonDataService personDS;
        ListView appDataListView;
        List<Person> appData;

        public MainActivity()
        {
            personDS = new PersonDataService();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.Main);
            SetTitle(Resource.String.EntryList);

            appDataListView = FindViewById<ListView>(Resource.Id.appDataListView);

            appData = personDS.GetPeople().ToList();

            DebtListViewAdapter adapter = new DebtListViewAdapter(this, this, appData);
            appDataListView.Adapter = adapter;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.MainMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int itemId = item.ItemId;

            if(itemId == Resource.Id.MainMenuAdd)
            {
                var intent = new Intent(this, typeof(EditActivity));
                this.StartActivity(intent);
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}

