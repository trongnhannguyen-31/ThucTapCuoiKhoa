using Android.Content;
using Phoenix.Droid.Controls;
using Phoenix.Framework.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessPicker), typeof(BorderlessPickerRenderer))]
namespace Phoenix.Droid.Controls
{
    public class BorderlessPickerRenderer : PickerRenderer
    {
        public BorderlessPickerRenderer(Context context) : base(context)
        {
        }

        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;

                var baseEntry = (BorderlessPicker)Element;
                Control.SetTextSize(global::Android.Util.ComplexUnitType.Sp, (float)baseEntry.FontSize);
	            Control.SetHintTextColor(Android.Graphics.Color.ParseColor(baseEntry.PlaceHolderTextColor)); 
				 var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                SetPadding(0, 0, 0, 0);
            }
        }
    }
}