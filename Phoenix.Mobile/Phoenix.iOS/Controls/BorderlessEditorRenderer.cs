using System.ComponentModel;
using CondDongBau.iOS.Controls;
using Phoenix.Framework.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]
namespace CondDongBau.iOS.Controls
{
    public class BorderlessEditorRenderer : EditorRenderer
    {
        
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control!=null && Control.Layer != null)
            {
                Control.Layer.BorderWidth = 0;
                
            }
            
        }
	  
	}
}