using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phoenix.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommonInputMultiple : ContentView
    {
        public CommonInputMultiple()
        {
            InitializeComponent();
        }

        #region Text
        public string Text
        {
            get => base.GetValue(TextProperty)?.ToString();
            set => base.SetValue(TextProperty, value);
        }

        public static BindableProperty TextProperty = BindableProperty.Create(
            propertyName: "Text",
            returnType: typeof(string),
            declaringType: typeof(CommonInputMultiple),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay);
        #endregion

        #region Background

        public Color Background
        {
            get => (Color)base.GetValue(BackgroundProperty);
            set => base.SetValue(BackgroundProperty, value);
        }

        public static BindableProperty BackgroundProperty = BindableProperty.Create(
            propertyName: "Background",
            returnType: typeof(Color),
            defaultValue: new Color(1, 1, 1),
            declaringType: typeof(CommonInputMultiple),
            defaultBindingMode: BindingMode.OneWay);

        #endregion

        #region TextColor

        public Color TextColor
        {
            get => (Color)base.GetValue(TextColorProperty);
            set => base.SetValue(TextColorProperty, value);
        }

        public static BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: "TextColor",
            returnType: typeof(Color),
            defaultValue: new Color(1, 1, 1),
            declaringType: typeof(CommonInputMultiple),
            defaultBindingMode: BindingMode.OneWay);

        #endregion

        #region Placeholder

        public string Placeholder
        {
            get => base.GetValue(PlaceholderProperty)?.ToString();
            set => base.SetValue(PlaceholderProperty, value);
        }

        public static BindableProperty PlaceholderProperty = BindableProperty.Create(
            propertyName: "Placeholder",
            returnType: typeof(string),
            declaringType: typeof(CommonInputMultiple),
            defaultValue: "",
            defaultBindingMode: BindingMode.OneWay);

        #endregion

        #region PlaceholderColor

        public Color PlaceholderColor
        {
            get => (Color)base.GetValue(PlaceholderColorProperty);
            set => base.SetValue(PlaceholderColorProperty, value);
        }

        public static BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            propertyName: "PlaceholderColor",
            returnType: typeof(Color),
            defaultValue: new Color(1, 1, 1),
            declaringType: typeof(CommonInputMultiple),
            defaultBindingMode: BindingMode.OneWay);

        #endregion

        #region ControlPadding
        public Thickness ControlPadding
        {
            get => (Thickness)base.GetValue(ControlPaddingProperty);
            set => base.SetValue(ControlPaddingProperty, value);
        }

        public static BindableProperty ControlPaddingProperty = BindableProperty.Create(
            propertyName: "ControlPadding",
            returnType: typeof(Thickness),
            declaringType: typeof(CommonInputMultiple),
            defaultValue: new Thickness(0),
            defaultBindingMode: BindingMode.OneWay);
        #endregion

        #region Radius
        public double Radius
        {
            get => (double)base.GetValue(RadiusProperty);
            set => base.SetValue(RadiusProperty, value);
        }

        public static BindableProperty RadiusProperty = BindableProperty.Create(
            propertyName: "Radius",
            returnType: typeof(double),
            declaringType: typeof(CommonInputMultiple),
            defaultValue: 0.0,
            defaultBindingMode: BindingMode.OneWay);
        #endregion

        #region HasShadow
        public bool HasShadow
        {
            get => (bool)base.GetValue(HasShadowProperty);
            set => base.SetValue(HasShadowProperty, value);
        }

        public static BindableProperty HasShadowProperty = BindableProperty.Create(
            propertyName: "HasShadow",
            returnType: typeof(bool),
            declaringType: typeof(CommonInputMultiple),
            defaultValue: false,
            defaultBindingMode: BindingMode.OneWay);
        #endregion

        #region HeightInput
        public double HeightInput
        {
            get => (double)base.GetValue(HeightInputProperty);
            set => base.SetValue(HeightInputProperty, value);
        }

        public static BindableProperty HeightInputProperty = BindableProperty.Create(
            propertyName: "HeightInput",
            returnType: typeof(double),
            declaringType: typeof(CommonInputMultiple),

            defaultBindingMode: BindingMode.OneWay);
        #endregion

        #region Keyboard
        public Keyboard Keyboard
        {
            get => (Keyboard)base.GetValue(KeyboardProperty);
            set => base.SetValue(KeyboardProperty, value);
        }

        public static BindableProperty KeyboardProperty = BindableProperty.Create(
            propertyName: "Keyboard",
            returnType: typeof(Keyboard),
            declaringType: typeof(CommonInputMultiple),
            defaultBindingMode: BindingMode.OneWay);
        #endregion      

        #region BorderColor

        public Color BorderColor
        {
            get => (Color)base.GetValue(BorderColorProperty);
            set => base.SetValue(BorderColorProperty, value);
        }

        public static BindableProperty BorderColorProperty = BindableProperty.Create(
            propertyName: "BorderColor",
            returnType: typeof(Color),
            defaultValue: new Color(1, 1, 1),
            declaringType: typeof(CommonInputMultiple),
            defaultBindingMode: BindingMode.OneWay);

        #endregion

        #region FontSize
        public double FontSize
        {
            get => (double)base.GetValue(FontSizeProperty);
            set => base.SetValue(FontSizeProperty, value);
        }

        public static BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: "FontSize",
            returnType: typeof(double),
            declaringType: typeof(CommonInputMultiple),
            defaultValue: 20.0,
            defaultBindingMode: BindingMode.TwoWay);
        #endregion
    }
}