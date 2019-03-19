using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes; 
using Microsoft.AppCenter.Distribute;
using Android.Content;

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
            newButton.Click += NewButton_Click;

            if (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                var deleteButton = FindViewById<ImageView>(Resource.Id.deleteButton);

                deleteButton.Click += DeleteButton_Click;
            }

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "note";
        }

        protected override void OnPostResume()
        {
            base.OnPostResume();

            var titlesFrag = FragmentManager.FindFragmentById<TitlesFragment>(Resource.Id.titles);

            titlesFrag.UpdateTitles();
        }

        private void DeleteButton_Click(object sender, System.EventArgs e)
        {
            if (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                DatabaseService dbService = new DatabaseService();

                var item = dbService.GetAllNotes()[NoteFragment.staticPlayId];

                dbService.RemoveNote(item);

                var titlesFrag = FragmentManager.FindFragmentById<TitlesFragment>(Resource.Id.titles);

                titlesFrag.UpdateTitles();
            }
        }

        private void EditButton_Click(object sender, System.EventArgs e)
        {
            if (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                DatabaseService dbService = new DatabaseService();

                var item = dbService.GetAllNotes()[NoteFragment.staticPlayId];

                dbService.RemoveNote(item);

                var titlesFrag = FragmentManager.FindFragmentById<TitlesFragment>(Resource.Id.titles);

                var intent = new Intent(this, typeof(NoteActivity));
                intent.PutExtra("current_play_id", NoteFragment.staticPlayId);
                StartActivity(intent);
            }
        }

        private void NewButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(AddNoteActivity));
            StartActivity(intent);
        }
    }
}