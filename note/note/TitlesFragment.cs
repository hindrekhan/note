using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace note
{
    public class TitlesFragment : ListFragment
    {
        int selectedPlayId;
        DatabaseService dbService;

        public TitlesFragment()
        {
            dbService = new DatabaseService();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            var notes = dbService.GetAllNotes();

            List<string> items = new List<string>();
            foreach (var note in notes)
            {
                items.Add(note.Title);
            }

            ListAdapter = new ArrayAdapter<String>(Activity,
                Android.Resource.Layout.SimpleListItemActivated1,
                items);

            if (savedInstanceState != null)
            {
                selectedPlayId = savedInstanceState.GetInt("current_play_id", 0);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("current_play_id", selectedPlayId);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            ShowPlayQuote(position);
        }

        private void ShowPlayQuote(int playId)
        {
            var intent = new Intent(Activity, typeof(NoteActivity));
            intent.PutExtra("current_play_id", playId);
            StartActivity(intent);
        }
    }
}