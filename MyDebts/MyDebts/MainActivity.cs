using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MyDebts.Entities;
using MyDebts.Resources;

namespace MyDebts
{
    [Activity(Label = "MyDebts", MainLauncher = true, Icon = "@drawable/icon")]
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
            appData.Add(new Person() { Name = "Maciek", When = DateTime.Now, Amount = 0.80f, Comment = "Za ksero", OwesMe = true });
            appData.Add(new Person() { Name = "Alan", When = DateTime.Now, Amount = 15.0f, Comment = "Za pizze", OwesMe = false });
            appData.Add(new Person() { Name = "Tomek", When = DateTime.Now, Amount = 300f, Comment = "", OwesMe = true });

            DebtListViewAdapter adapter = new DebtListViewAdapter(this, appData);
            appDataListView.Adapter = adapter;
        }
    }
}

