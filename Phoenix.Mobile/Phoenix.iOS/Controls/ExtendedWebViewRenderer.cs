using CongDongBau.Controls;
using CongDongBau.iOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(ExtendedWebViewRenderer))]
namespace CongDongBau.iOS.Controls
{
    public class ExtendedWebViewRenderer : WkWebViewRenderer
    {
        //WKWebView webView;
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            //webView.NavigationDelegate= new ExtendedUIWebViewDelegate(this);
            if (!(e.NewElement is ExtendedWebView extendedWeb)) return;
            if (extendedWeb.EnableCustomFixScroll)
            {
                NavigationDelegate = new ExtendedUIWebViewDelegate(this);
            }
            else
            {
                ScrollView.ScrollEnabled = extendedWeb.Bounces;
                ScrollView.Bounces = extendedWeb.Bounces;
            }
        }
    }
}