using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes; 
using Microsoft.AppCenter.Distribute;

namespace note
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppCenter.Start("5804bf2e-3240-4b18-b6d8-ef8fdc16762e", typeof(Distribute));
            AppCenter.Start("5804bf2e-3240-4b18-b6d8-ef8fdc16762e", typeof(Analytics), typeof(Crashes));

            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            DatabaseService dbService = new DatabaseService();
            dbService.CreateTableWithData();

            var newButton = FindViewById<ImageView>(Resource.Id.newButton);
            var editButton = FindViewById<ImageView>(Resource.Id.editButton);
            var deleteButton = FindViewById<ImageView>(Resource.Id.deleteButton);

            newButton.Click += NewButton_Click;
            editButton.Click += EditButton_Click;
            deleteButton.Click += DeleteButton_Click;

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "note";
        }

        private void DeleteButton_Click(object sender, System.EventArgs e)
        {
            var a = 0;
            var b = a;
        }

        private void EditButton_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void NewButton_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}