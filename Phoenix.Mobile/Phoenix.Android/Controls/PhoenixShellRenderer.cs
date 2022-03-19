using Android.Content;
using Phoenix.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Shell), typeof(PhoenixShellRenderer))]
namespace Phoenix.Droid.Controls
{
    public class PhoenixShellRenderer : ShellRenderer
    {
        public PhoenixShellRenderer(Context context) : base(context)
        {
        }

        protected override IShellItemRenderer CreateShellItemRenderer(ShellItem shellItem)
        {
            return new PhoenixShellItemRenderer(this);
        }
    }
}
