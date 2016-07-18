using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using MyDebts.Entities;

namespace MyDebts.Activities
{
    [Activity(Label = "Edytuj wpis", Icon = "@drawable/edit")]
    public class EditActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Edit);

            var person = JsonConvert.DeserializeObject<Person>(Intent.GetStringExtra("person"));
            BindControls(person);
        }

        private void BindControls(Person person)
        {
            this.FindViewById<TextView>(Resource.Id.EntryName).Text = person.Name;
            this.FindViewById<TextView>(Resource.Id.EntryAmount).Text = person.Amount.ToString();
            this.FindViewById<DatePicker>(Resource.Id.EntryWhen).DateTime = person.When;
            this.FindViewById<ToggleButton>(Resource.Id.EntryOwesMe).Selected = person.OwesMe;
        }
    }
}