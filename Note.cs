using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace notes_app
{
    public class Note
    {
        public string content { get; set; }
        public string title { get; set; }
        public string time { get; set; }
    }
}