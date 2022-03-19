using Android.Content;
using Phoenix.Droid.Controls;
using Phoenix.Framework.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace Phoenix.Droid.Controls
{
	public class BorderlessEntryRenderer: EntryRenderer
	{
		public BorderlessEntryRenderer(Context context) : base(context)
		{
		}
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
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
                //function for go next 
		        var baseEntry = (BorderlessEntry)Element;
		        Control.ImeOptions = baseEntry.ReturnKeyType.GetValueFromDescription();
		        //// This is hackie ;-) / A Android-only bindable property should be added to the EntryExt class 
		        Control.SetImeActionLabel(baseEntry.ReturnKeyType.ToString(), Control.ImeOptions);
		        Control.EditorAction += (sender, args) =>
		        {
		            baseEntry.EntryActionFired();
		        };
            }
		}
	}
}