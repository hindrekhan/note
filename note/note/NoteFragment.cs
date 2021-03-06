﻿using System;
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
    public class NoteFragment : Fragment
    {
        public int PlayId => Arguments.GetInt("current_play_id", 0);
        public static int staticPlayId { get; set; }
        DatabaseService dbService;

        public static NoteFragment NewInstance (int playId)
        {
            var bundle = new Bundle();
            bundle.PutInt("current_play_id", playId);

            return new NoteFragment { Arguments = bundle };
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            dbService = new DatabaseService();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }

            staticPlayId = PlayId;

            var textView = new TextView(Activity);
            var padding = Convert.ToInt32(
                TypedValue.ApplyDimension(ComplexUnitType.Dip, 4,
                Activity.Resources.DisplayMetrics));

            textView.SetPadding(padding, padding, padding, padding);
            textView.TextSize = 24;
            try
            {
                textView.Text = dbService.GetAllNotes().ElementAt(PlayId).Content;
            }
            catch
            {

            }

            var scroller = new ScrollView(Activity);
            scroller.AddView(textView);

            return scroller;
        }
    }
}