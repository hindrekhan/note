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
        bool showingTwoFragments;

        public TitlesFragment()
        {
            dbService = new DatabaseService();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            UpdateTitles();

            if (savedInstanceState != null)
            {
                selectedPlayId = savedInstanceState.GetInt("current_play_id", 0);
            }

            var noteContainer = Activity.FindViewById(Resource.Id.note_container);
            showingTwoFragments = noteContainer != null &&
                                  noteContainer.Visibility == ViewStates.Visible;

            if (showingTwoFragments)
            {
                ListView.ChoiceMode = ChoiceMode.Single;
                ShowNote(selectedPlayId);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("current_play_id", selectedPlayId);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            ShowNote(position);
        }

        private void ShowNote(int playId)
        {
            selectedPlayId = playId;
            if (showingTwoFragments)
            {
                ListView.SetItemChecked(selectedPlayId, true);

                var noteFragment = FragmentManager.FindFragmentById(Resource.Id.note_container) as NoteFragment;

                if (noteFragment == null || noteFragment.PlayId != playId)
                {
                    var container = Activity.FindViewById(Resource.Id.note_container);
                    var noteFrag = NoteFragment.NewInstance(selectedPlayId);

                    FragmentTransaction ft = FragmentManager.BeginTransaction();
                    ft.Replace(Resource.Id.note_container, noteFrag);
                    ft.AddToBackStack(null);
                    ft.SetTransition(FragmentTransit.FragmentFade);
                    ft.Commit();
                }
            }
            else
            {
                var intent = new Intent(Activity, typeof(NoteActivity));
                intent.PutExtra("current_play_id", playId);
                StartActivity(intent);
            }
        }

        public void UpdateTitles()
        {
            var notes = dbService.GetAllNotes();

            List<string> items = new List<string>();
            foreach (var note in notes)
            {
                items.Add(note.Title);
            }

            ListAdapter = new ArrayAdapter(Activity,
                Android.Resource.Layout.SimpleListItemActivated1,
                items);
        }
    }
}