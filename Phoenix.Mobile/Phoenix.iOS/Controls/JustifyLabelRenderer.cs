using CongDongBau.Controls;
using CongDongBau.iOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(JustifyLabel), typeof(JustifyLabelRenderer))]
namespace CongDongBau.iOS.Controls
{
    public class JustifyLabelRenderer : LabelRenderer
    {
        
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.TextAlignment = UITextAlignment.Justified;
            }
        }
    }
}