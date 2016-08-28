using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using MyDebts.Entities;
using System;
using MyDebts.DB.DataServices;
using System.Globalization;
using MyDebts.Logic;

namespace MyDebts.Activities
{
    [Activity(Icon = "@drawable/IcoUserEdit")]
    public class EditActivity : Activity
    {
        PersonDataService personDS = null;

        public EditActivity()
        {
            personDS = new PersonDataService();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Edit);

            var id = Intent.GetIntExtra("id", -1);
            BindControls(id);
        }

        private void BindControls(int id)
        {
            var person = personDS.GetPerson(id);
            if (person != null)
            {
                this.FindViewById<TextView>(Resource.Id.EntryName).Text = person.Name;
                this.FindViewById<TextView>(Resource.Id.EntryAmount).Text = person.Amount.ToString();
                this.FindViewById<TextView>(Resource.Id.EntryComment).Text = person.Comment.ToString();
                this.FindViewById<Button>(Resource.Id.EntryWhen).Text = person.When.ToLongDateString();
                this.FindViewById<Switch>(Resource.Id.EntryOwesMe).Checked = person.MyDebt;
                BindDelete(id);
                this.SetTitle(Resource.String.EntryEdit);
            }
            else
            {
                id = personDS.GetNextId();
                HideDelete();
                this.SetTitle(Resource.String.EntryAdd);
            }
            BindEditDate();
            BindSave(id);
        }

        private void BindEditDate()
        {
            var editDateBtn = this.FindViewById<Button>(Resource.Id.EntryWhen);

            DateTime dateToPass = new DateTime();
            try
            {
                dateToPass = DateTime.Parse(editDateBtn.Text);
            }
            catch (Exception)
            {
                dateToPass = DateTime.Now;
            }   

            editDateBtn.Click += (object sender, EventArgs e) =>
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    editDateBtn.Text = time.ToLongDateString();
                }, dateToPass);
                frag.Show(FragmentManager, DatePickerFragment.TAG);
            };
        }

        private void BindSave(int id)
        {
            CultureInfo cultureInfo = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";

            var saveBtn = this.FindViewById<Button>(Resource.Id.EntrySave);
            saveBtn.Click += (object sender, EventArgs e) =>
            {
                float amount = float.TryParse(this.FindViewById<TextView>(Resource.Id.EntryAmount).Text, NumberStyles.Any, cultureInfo, out amount) ? amount : 0.0f;
                DateTime date = DateTime.TryParse(this.FindViewById<Button>(Resource.Id.EntryWhen).Text, out date) ? date : DateTime.Now;

                var person = new Person
                {
                    Id = id,
                    Name = this.FindViewById<TextView>(Resource.Id.EntryName).Text,
                    Amount = amount,
                    When = date,
                    MyDebt = this.FindViewById<Switch>(Resource.Id.EntryOwesMe).Checked,
                    Comment = this.FindViewById<TextView>(Resource.Id.EntryComment).Text
                };
                
                personDS.AddUpdatePerson(person);
                NavigateBack();
            };
        }

        private void BindDelete(int id)
        {
            var deleteBtn = this.FindViewById<Button>(Resource.Id.EntryDelete);
            deleteBtn.Click += (object sender, EventArgs e) =>
            {
                new AlertDialog.Builder(this)
                .SetPositiveButton("Tak", (sender1, args1) =>
                {
                    personDS.DeletePerson(id);
                    NavigateBack();
                })
                .SetNegativeButton("Nie", (sender2, args2) =>
                {
                    // User pressed no 
                })
                .SetMessage("Czy na pewno usunπÊ?")
                .SetTitle("Potwierdü")
                .Show();

            };
        }

        private void HideDelete()
        {
            this.FindViewById<Button>(Resource.Id.EntryDelete).Visibility = Android.Views.ViewStates.Invisible;
        }

        private void NavigateBack()
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop);
            this.StartActivity(intent);
            this.Finish();
        }
    }
}