using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace note
{
    class DatabaseService
    {
        SQLiteConnection db;

        public DatabaseService()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal),
                "mydatabase.db1");

            db = new SQLiteConnection(dbPath);
        }

        public void CreateDatabase()
        {
            db.CreateTable<Note>();
        }
        
        public void AddNote(Note note)
        {
            db.Insert(note);
        }

        public void UpdateNote(Note note)
        {
            db.Update(note);
        }

        public void CreateTableWithData()
        {
            db.CreateTable<Note>();

            if (db.Table<Note>().Count() == 0)
            {
                var newNotes = new Note();
                newNotes.Title = "asdbf";
                newNotes.Content = "123";
                newNotes.Id = 1;
                db.Insert(newNotes);
                newNotes.Title = "11111";
                newNotes.Content = "laaa";
                newNotes.Id = 2;
                db.Insert(newNotes);
            }
        }

        public List<Note> GetAllNotes()
        {
            var table = db.Table<Note>();

            return table.ToList();
        }

        public void RemoveNote(Note note)
        {
            db.Delete<Note>(note.Id);
        }
    }
}