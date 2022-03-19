using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("CondDongBau")]
[assembly: ExportEffect(typeof(CondDongBau.iOS.Effects.SafeAreaPaddingEffect), nameof(CondDongBau.iOS.Effects.SafeAreaPaddingEffect))]
namespace CondDongBau.iOS.Effects
{
    class SafeAreaPaddingEffect : PlatformEffect
    {
        Thickness _padding;
        protected override void OnAttached()
        {
            if (Element is Layout element)
            {
                _padding = element.Padding;
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    
                    var insets = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;

                    if (insets.Top > 0)
                    {
                        element.Padding = new Thickness(_padding.Left + insets.Left, _padding.Top + insets.Top,
                            _padding.Right + insets.Right, _padding.Bottom + insets.Bottom);
                        return;
                    }
                }
                element.Padding = new Thickness(_padding.Left, _padding.Top + 20, _padding.Right, _padding.Bottom);
            }
        }

        protected override void OnDetached()
        {
            if (Element is Layout element)
            {
                element.Padding = _padding;
            }
        }
    }
}