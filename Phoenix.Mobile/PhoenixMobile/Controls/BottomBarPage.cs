using Xamarin.Forms;

namespace Phoenix.Mobile.Controls
{
    public class BottomBarPage : TabbedPage
    {
        public enum BarThemeTypes { Light, DarkWithAlpha, DarkWithoutAlpha }
        public bool FixedMode { get; set; }
        public BarThemeTypes BarTheme { get; set; }
        public Color BarActiveTabColor { get; set; }
        public void RaiseCurrentPageChanged()
        {
            OnCurrentPageChanged();
        }
    }
}