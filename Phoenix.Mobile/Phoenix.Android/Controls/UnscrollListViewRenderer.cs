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
using Phoenix.Mobile.Controls;
using Phoenix.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(UnscrollListView), typeof(UnscrollListViewRenderer))]
namespace Phoenix.Droid.Controls
{
    public class UnscrollListViewRenderer : ListViewRenderer
    {
        public UnscrollListViewRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            if (Control != null)
            {
                Control.SetScrollContainer(false);
            }
        }
    }
}