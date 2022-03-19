using Xamarin.Forms;

namespace Phoenix.Mobile.Controls
{
    public class TopTabbedPage : TabbedPage
    {
        public static readonly BindableProperty BarIndicatorColorProperty = BindableProperty.Create(
            nameof(BarIndicatorColor),
            typeof(Color),
            typeof(TopTabbedPage),
            Color.White,
            BindingMode.OneWay);
        public Color BarIndicatorColor
        {
            get { return (Color)GetValue(BarIndicatorColorProperty); }
            set { SetValue(BarIndicatorColorProperty, value); }
        }


        public static readonly BindableProperty SwipeEnabledColorProperty = BindableProperty.Create(
            nameof(SwipeEnabled),
            typeof(bool),
            typeof(TopTabbedPage),
            true,
            BindingMode.OneWay);
        public bool SwipeEnabled
        {
            get { return (bool)GetValue(SwipeEnabledColorProperty); }
            set { SetValue(SwipeEnabledColorProperty, value); }
        }
        #region FontFamily

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create("FontFamily", typeof(string), typeof(TopTabbedPage), default(string));

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        #endregion
    }
}
