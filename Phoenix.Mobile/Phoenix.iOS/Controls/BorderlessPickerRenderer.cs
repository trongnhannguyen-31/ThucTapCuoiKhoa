using System.ComponentModel;
using Foundation;
using CondDongBau.iOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Phoenix.Framework.Controls;

[assembly: ExportRenderer(typeof(BorderlessPicker), typeof(BorderlessPickerRenderer))]
namespace CondDongBau.iOS.Controls
{
    public class BorderlessPickerRenderer : PickerRenderer
    {
        
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null && Control.Layer != null)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
            }
		}
	    protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
	    {
		    base.OnElementChanged(e);
            if (e == null) return;
		    if ( e.NewElement != null)
		    {
			    var customPicker = e.NewElement as BorderlessPicker;

			    string placeholderColor = customPicker.PlaceHolderTextColor;
			    UIColor color = UIColor.FromRGB(GetRed(placeholderColor), GetGreen(placeholderColor), GetBlue(placeholderColor));

			    var placeholderAttributes = new NSAttributedString(customPicker.Title??"", new UIStringAttributes()
				    { ForegroundColor = color });
                if (Control !=null)
			    Control.AttributedPlaceholder = placeholderAttributes;
		    }
	    }
	    private float GetRed(string color)
	    {
		    Color c = Color.FromHex(color);
		    return (float)c.R;
	    }

	    private float GetGreen(string color)
	    {
		    Color c = Color.FromHex(color);
		    return (float)c.G;
	    }

	    private float GetBlue(string color)
	    {
		    Color c = Color.FromHex(color);
		    return (float)c.B;
	    }
	}
}