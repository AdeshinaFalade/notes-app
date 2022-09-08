
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace notes_app
{
    public class NoteAdapter : RecyclerView.Adapter
    {
        public event EventHandler<NoteAdapterClickEventArgs> ItemClick;
        public event EventHandler<NoteAdapterClickEventArgs> ItemLongClick;
        List<Note> NoteList;

        public NoteAdapter(List<Note> data)
        {
            NoteList = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.recycler_layout;
            itemView = LayoutInflater.From(parent.Context).
                Inflate(id, parent, false);

            var vh = new NoteAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var note = NoteList[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as NoteAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.textTitle.Text = note.content;
            holder.txtContent.Text = GetValue(note.content);
            holder.txtTime.Text = note.time;
        }
        private string GetValue(string title)
        {
            string value = string.Empty;
            var array = title.Split(" ");
            if (array.Count() > 15)
            {
                for (int i = 10; i < 15; i++)
                {
                    value += array[i] + " ";
                }
            }
            else
                value = "No additional content";

            return value;
        }

       
        public override int ItemCount => NoteList.Count;

        void OnClick(NoteAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(NoteAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class NoteAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView textTitle;
        public TextView txtTime;
        public TextView txtContent;

        public NoteAdapterViewHolder(View itemView, Action<NoteAdapterClickEventArgs> clickListener,
                            Action<NoteAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            textTitle = itemView.FindViewById<TextView>(Resource.Id.textTitle);
            txtTime = itemView.FindViewById<TextView>(Resource.Id.txtTime);
            txtContent = itemView.FindViewById<TextView>(Resource.Id.txtContent);
            itemView.Click += (sender, e) => clickListener(new NoteAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new NoteAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class NoteAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}