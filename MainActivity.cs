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

namespace notes_app
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText edtSearch;
        TextView txtCount;
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
            txtCount = FindViewById<TextView>(Resource.Id.txtCount);
            edtSearch = FindViewById<EditText>(Resource.Id.edtSearch);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView1);
            imgNew.Click += ImgNew_Click;
            Note_Activity.RetrieveData();

            SetupRecyclerView();




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
             
        }

        private void Adapter_ItemClick(object sender, NoteAdapterClickEventArgs e)
        {
           
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