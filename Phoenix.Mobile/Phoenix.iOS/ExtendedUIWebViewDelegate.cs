using CongDongBau.Controls;
using CongDongBau.iOS.Controls;
using WebKit;
using Xamarin.Forms;

namespace CongDongBau.iOS
{
    public class ExtendedUIWebViewDelegate : WKNavigationDelegate
    {
        private readonly ExtendedWebViewRenderer _webViewRenderer;

        public ExtendedUIWebViewDelegate(ExtendedWebViewRenderer webViewRenderer = null)
        {
            this._webViewRenderer = webViewRenderer ?? new ExtendedWebViewRenderer();
        }

        public override async void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            //base.DidFinishNavigation(webView, navigation);
            if (!(_webViewRenderer.Element is ExtendedWebView wv) || !(wv.HeightRequest < 0)) return;
            await System.Threading.Tasks.Task.Delay(wv.TimeReloadScroll); // wait here till content is rendered
            wv.HeightRequest = webView.ScrollView.ContentSize.Height;
            _webViewRenderer.Element.HeightRequest = wv.HeightRequest;
            if (!(wv.Parent.Parent is ViewCell vc)) return;
            vc.ForceUpdateSize();
            ((ListView)vc.Parent).HeightRequest = wv.HeightRequest;
        }

        //public override async void LoadingFinished(UIWebView webView)
        //{
        //    if (_webViewRenderer.Element is ExtendedWebView wv && wv.HeightRequest<0)
        //    {
        //        await System.Threading.Tasks.Task.Delay(500); // wait here till content is rendered
        //        wv.HeightRequest = (double)webView.ScrollView.ContentSize.Height;
        //        if(wv.Parent.Parent is ViewCell vc)
        //        {
        //            vc.ForceUpdateSize();
        //            ((ListView) vc.Parent).HeightRequest = wv.HeightRequest;
        //        }    

        //    }
        //}
    }
}