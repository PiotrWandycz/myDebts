using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.OS;
using MyDebts.Entities;
using MyDebts.Resources;

namespace MyDebts.Activities
{
    [Activity(MainLauncher = true, Label = "Lista długów", Icon = "@drawable/appIcon")]
    public class MainActivity : Activity
    {
        ListView appDataListView;
        List<Person> appData;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.Main);

            appDataListView = FindViewById<ListView>(Resource.Id.appDataListView);

            appData = new List<Person>();
            appData.Add(new Person() { Id = Guid.NewGuid(), Name = "Maciek", When = DateTime.Now, Amount = 0.80f, Comment = "Za ksero", OwesMe = true });
            appData.Add(new Person() { Id = Guid.NewGuid(), Name = "Alan", When = DateTime.Now, Amount = 15.0f, Comment = "Za pizze", OwesMe = false });
            appData.Add(new Person() { Id = Guid.NewGuid(), Name = "Tomek", When = DateTime.Now, Amount = 300f, Comment = "", OwesMe = true });

            DebtListViewAdapter adapter = new DebtListViewAdapter(this, this, appData);
            appDataListView.Adapter = adapter;
        }
    }
}

