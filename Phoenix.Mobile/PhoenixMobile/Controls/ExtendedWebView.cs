using Xamarin.Forms;

namespace Phoenix.Mobile.Controls
{
    public class ExtendedWebView : WebView
    {
        private static readonly BindableProperty EnableCustomFixScrollProperty =
            BindableProperty.Create(nameof(EnableCustomFixScroll), typeof(bool), typeof(ExtendedWebView), true, BindingMode.TwoWay);
        public bool EnableCustomFixScroll
        {
            get => (bool)GetValue(EnableCustomFixScrollProperty);
            set => SetValue(EnableCustomFixScrollProperty, value);
        }

        private static readonly BindableProperty TimeReloadScrollProperty =
            BindableProperty.Create(nameof(TimeReloadScroll), typeof(int), typeof(ExtendedWebView), 500, BindingMode.TwoWay);
        public int TimeReloadScroll
        {
            get => (int)GetValue(TimeReloadScrollProperty);
            set => SetValue(TimeReloadScrollProperty, value);
        }
        private static readonly BindableProperty BouncesProperty =
            BindableProperty.Create(nameof(Bounces), typeof(bool), typeof(ExtendedWebView), true, BindingMode.TwoWay);
        public bool Bounces
        {
            get => (bool)GetValue(BouncesProperty);
            set => SetValue(BouncesProperty, value);
        }
        
    }
}
