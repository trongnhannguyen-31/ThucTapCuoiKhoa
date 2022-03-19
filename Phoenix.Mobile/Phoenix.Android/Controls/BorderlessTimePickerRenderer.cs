using Android.Content;
using Phoenix.Droid.Controls;
using Phoenix.Framework.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TimePicker = Xamarin.Forms.TimePicker;

[assembly: ExportRenderer(typeof(BorderlessTimePicker), typeof(BorderlessTimePickerRenderer))]
namespace Phoenix.Droid.Controls
{
    public class BorderlessTimePickerRenderer : TimePickerRenderer
    {
        public BorderlessTimePickerRenderer(Context context) : base(context)
        {
        }

        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;

                var baseEntry = (BorderlessTimePicker)Element;
                Control.SetTextSize(global::Android.Util.ComplexUnitType.Sp, (float)baseEntry.FontSize);

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