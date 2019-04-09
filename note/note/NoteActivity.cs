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

namespace note
{
    [Activity(Label = "NoteActivity")]
    public class NoteActivity : Activity
    {
        Note note;
        EditText title;
        EditText content;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                Finish();
            }

            SetContentView(Resource.Layout.activity_edit);

            var playId = Intent.Extras.GetInt("current_play_id", 0);

            var dbService = new DatabaseService();
            note = dbService.GetAllNotes()[playId];


            title = FindViewById<EditText>(Resource.Id.title);
            content = FindViewById<EditText>(Resource.Id.content);
        
            title.Text = note.Title;
            content.Text = note.Content;

            var addButton = FindViewById<ImageView>(Resource.Id.buttonAdd);
            addButton.Click += AddButton_Click;

            var delButton = FindViewById<ImageView>(Resource.Id.buttonEditDel);
            delButton.Click += DelButton_Click;
        }

        private void DelButton_Click(object sender, EventArgs e)
        {
            DatabaseService dbService = new DatabaseService();

            dbService.RemoveNote(note);

            Finish();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            DatabaseService dbService = new DatabaseService();

            note.Content = content.Text;
            note.Title = title.Text;

            dbService.UpdateNote(note);
 
            Finish();
        }
    }
}