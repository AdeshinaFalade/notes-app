using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace notes_app
{
    [Activity(Label = "Note_Activity")]
    public class Note_Activity : Activity
    {
        public static string contents;
        EditText editText;
        TextView textTitle;
        TextView txtNoteTime;



        public static ISharedPreferences pref = Application.Context.GetSharedPreferences("note", FileCreationMode.Private);
        public static ISharedPreferencesEditor editor;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your application here
            SetContentView(Resource.Layout.padLayout);
            editText = FindViewById<EditText>(Resource.Id.edtNote);
            textTitle = FindViewById<TextView>(Resource.Id.textTitle);
            txtNoteTime = FindViewById<TextView>(Resource.Id.txtNoteTime);
            editText.Text = Intent.GetStringExtra("id");
            txtNoteTime.Text = DateTime.Now.ToLongDateString();

        }
        public override void OnBackPressed()
        {
            string  time;
            contents = editText.Text;
            time = DateTime.Now.ToShortDateString();
            Note note = new Note();
            note.content = contents;
            note.title = contents;
            note.time = time;
            editor = pref.Edit();

            if (string.IsNullOrEmpty(contents))
            {
                base.OnBackPressed();
            }
            else
            {


                if (MainActivity.listOfNotes.Count > 0)
                {


                    MainActivity.listOfNotes.Insert(0, note);
                    MainActivity.adapter.NotifyItemInserted(0);

                    string jsonString = JsonConvert.SerializeObject(MainActivity.listOfNotes);
                    editor.PutString("note", jsonString);
                    editor.Apply();
                }
                else
                {
                    MainActivity.listOfNotes.Add(note);
                    string jsonString = JsonConvert.SerializeObject(MainActivity.listOfNotes);
                    editor.PutString("note", jsonString);
                    editor.Apply();

                }
                base.OnBackPressed();
            }
        }

        public static void RetrieveData()
        {
            string json = pref.GetString("note", "");
            if (!string.IsNullOrEmpty(json))
            {
                MainActivity.listOfNotes = JsonConvert.DeserializeObject<List<Note>>(json);
            }
        }
    }
}