using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;

namespace notes_app
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText edtSearch;
        TextView txtCount;
        TextView txtPlaceholder;
        ImageView imgNew;
        public static List<Note> listOfNotes = new List<Note>();
        RecyclerView recyclerView;
        public static NoteAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            imgNew = FindViewById<ImageView>(Resource.Id.imgNew);
            txtPlaceholder = FindViewById<TextView>(Resource.Id.txtPlaceholder);
            txtCount = FindViewById<TextView>(Resource.Id.txtCount);
            edtSearch = FindViewById<EditText>(Resource.Id.edtSearch);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView1);
            imgNew.Click += ImgNew_Click;
            Note_Activity.RetrieveData();
            if (listOfNotes.Count > 0)
            {
                SetupRecyclerView();
            }
            else
            {
                txtPlaceholder.Visibility = Android.Views.ViewStates.Gone;
            }
        }

        protected override void OnResume()
        {
            Note_Activity.RetrieveData();
            SetupRecyclerView();
            base.OnResume();
        }



        public void SetupRecyclerView()
        {
            
            recyclerView.SetLayoutManager(new LinearLayoutManager (recyclerView.Context));
            adapter = new NoteAdapter(listOfNotes);
            adapter.ItemClick += Adapter_ItemClick;
            adapter.ItemLongClick += Adapter_ItemLongClick;
            txtCount.Text = listOfNotes.Count.ToString() + " Notes";
            recyclerView.SetAdapter(adapter);   

        }

        private void Adapter_ItemLongClick(object sender, NoteAdapterClickEventArgs e)
        {
            var note = listOfNotes[e.Position];
            AlertDialog.Builder deleteAlert = new AlertDialog.Builder(this);
            deleteAlert.SetMessage("Are you sure ?");
            deleteAlert.SetTitle("Delete note");
            deleteAlert.SetPositiveButton("Delete", (alert, args) =>
            {
                try
                {
                    listOfNotes.RemoveAt(e.Position);
                    adapter.NotifyItemRemoved(e.Position);

                    string jsonString = JsonConvert.SerializeObject(listOfNotes);
                    Note_Activity.editor.PutString("note", jsonString);
                    Note_Activity.editor.Apply();
                    txtCount.Text = listOfNotes.Count.ToString() + " Notes";
                }
                catch (Exception ex)
                {

                    var error = ex.Message ;
                }
            });


            deleteAlert.SetNegativeButton("Cancel", (alert, args) => { deleteAlert.Dispose(); });
            deleteAlert.Show();
            
        }

        private void Adapter_ItemClick(object sender, NoteAdapterClickEventArgs e)
        {
            var note = listOfNotes[e.Position];
            Intent intent = new Intent(this, typeof(Note_Activity));
            intent.PutExtra("id", note.content);
            
            StartActivity(intent);

            listOfNotes.RemoveAt(e.Position);
            adapter.NotifyItemRemoved(e.Position);
            string jsonString = JsonConvert.SerializeObject(listOfNotes);
            Note_Activity.editor.PutString("note", jsonString);
            Note_Activity.editor.Apply();
        }

       


        private void ImgNew_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this,typeof(Note_Activity));

            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}