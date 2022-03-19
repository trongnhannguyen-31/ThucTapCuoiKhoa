using Android.Content;
using Phoenix.Droid.Controls;
using Phoenix.Mobile.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(JustifyLabel), typeof(JustifyLabelRenderer))]
namespace Phoenix.Droid.Controls
{
    public class JustifyLabelRenderer : LabelRenderer
    {
        public JustifyLabelRenderer(Context context):base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                Control.JustificationMode = Android.Text.JustificationMode.InterWord;
            }    
        }
    }
}