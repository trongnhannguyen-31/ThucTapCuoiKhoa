using Android.Content;
using Phoenix.Droid.Controls;
using Phoenix.Framework.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessDatePicker), typeof(BorderlessDatePickerRenderer))]
namespace Phoenix.Droid.Controls
{
    public class BorderlessDatePickerRenderer : DatePickerRenderer
    {
        public BorderlessDatePickerRenderer(Context context) : base(context)
        {
        }

        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
                var baseEntry = (BorderlessDatePicker)Element;
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