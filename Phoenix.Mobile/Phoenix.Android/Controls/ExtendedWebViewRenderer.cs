using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Phoenix.Mobile.Controls;
using Phoenix.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WebView = Android.Webkit.WebView;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(ExtendedWebViewRenderer))]
namespace Phoenix.Droid.Controls
{
    public class ExtendedWebViewRenderer:WebViewRenderer
    {
        public ExtendedWebViewRenderer(Context context) : base(context)
        {

        }
        //protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        //{
        //    base.OnElementChanged(e);

        //    if (e.NewElement is WebView webViewControl)
        //    {
        //        if (e.OldElement == null)
        //        {
        //            Control.SetWebViewClient(new ExtendedWebViewClient(webViewControl));
        //        }
        //    }
        //}
        //class ExtendedWebViewClient : Android.Webkit.WebViewClient
        //{
        //    private readonly ExtendedWebView _control;

        //    public ExtendedWebViewClient(ExtendedWebView control)
        //    {
        //        _control = control;
        //    }

        //    public override async void OnPageFinished(WebView view, string url)
        //    {
        //        if (_control != null)
        //        {
        //            int i = 10;
        //            while (view.ContentHeight == 0 && i-- > 0) // wait here till content is rendered
        //            {
        //                await System.Threading.Tasks.Task.Delay(100);
        //            }

        //            _control.HeightRequest = view.ContentHeight;
        //        }

        //        base.OnPageFinished(view, url);
        //    }
        //}

        static ExtendedWebView _xwebView = null;
        WebView _webView;
        class ExtendedWebViewClient : Android.Webkit.WebViewClient
        {

            public override async void OnPageFinished(WebView view, string url)
            {
                if (_xwebView != null)
                {
                    int i = 10;
                    while (view.ContentHeight == 0 && i-- > 0) // wait here till content is rendered
                        await System.Threading.Tasks.Task.Delay(500);
                    _xwebView.HeightRequest = view.ContentHeight;
                    var vc = (_xwebView.Parent.Parent as ViewCell);
                    if (vc != null)
                    {
                        vc.ForceUpdateSize();
                        var lv = vc.Parent as Xamarin.Forms.ListView;
                        lv.HeightRequest = _xwebView.HeightRequest+20;
                    }
                }
                base.OnPageFinished(view, url);
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            _xwebView = e.NewElement as ExtendedWebView;
            _webView = Control;

            if (e.OldElement == null)
            {
                _webView.SetWebViewClient(new ExtendedWebViewClient());
            }

        }
    }
}