using System.ComponentModel;
using CondDongBau.iOS.Controls;
using Phoenix.Framework.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace CondDongBau.iOS.Controls
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control!=null){
                
                if (Control.Layer!=null)
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}



