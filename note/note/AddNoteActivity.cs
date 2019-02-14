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
    [Activity(Label = "AddNoteActivity")]
    public class AddNoteActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_addnote);

            var addButton = FindViewById<ImageView>(Resource.Id.buttonAdd);
            addButton.Click += AddButton_Click;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            DatabaseService dbService = new DatabaseService();

            var title = FindViewById<EditText>(Resource.Id.title);
            var content = FindViewById<EditText>(Resource.Id.content);

            Note note = new Note();
            note.Title = title.Text;
            note.Content = content.Text;

            dbService.AddNote(note);
            Finish();
        }
    }
}