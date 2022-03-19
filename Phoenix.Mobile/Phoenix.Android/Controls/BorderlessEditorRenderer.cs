using Android.Content;
using Phoenix.Droid.Controls;
using Phoenix.Framework.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]
namespace Phoenix.Droid.Controls
{
    public class BorderlessEditorRenderer : EditorRenderer
    {
        public BorderlessEditorRenderer(Context context) : base(context)
        {
        }

        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                SetPadding(0, 0, 0, 0);
			}

	  //      if (e.NewElement is BorderlessEditor element)
	  //      {
		 //       this.Control.Hint = element.Placeholder;
		 //       Control.SetHintTextColor(Android.Graphics.Color.ParseColor(element.PlaceHolderTextColor));
			//}

        }
    }
}