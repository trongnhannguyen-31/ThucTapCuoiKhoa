using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phoenix.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationBar : ContentView
    {
        public NavigationBar()
        {
            InitializeComponent();

        }

        #region Frame

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
            declaringType: typeof(NavigationBar),
            defaultBindingMode: BindingMode.OneWay);

        #endregion        

        #region HeightBar
        public double HeightBar
        {
            get => (double)base.GetValue(HeightBarProperty);
            set => base.SetValue(HeightBarProperty, value);
        }

        public static readonly BindableProperty HeightBarProperty = BindableProperty.Create(
            propertyName: "HeightBar",
            returnType: typeof(double),
            declaringType: typeof(NavigationBar),
            defaultValue: 40.0,
            defaultBindingMode: BindingMode.TwoWay);

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
            declaringType: typeof(NavigationBar),
            defaultValue: false,
            defaultBindingMode: BindingMode.OneWay);
        #endregion

        #region Radius
        public int Radius
        {
            get => (int)base.GetValue(RadiusProperty);
            set => base.SetValue(RadiusProperty, value);
        }

        public static BindableProperty RadiusProperty = BindableProperty.Create(
            propertyName: "Radius",
            returnType: typeof(int),
            declaringType: typeof(NavigationBar),
            defaultValue: 13,
            defaultBindingMode: BindingMode.OneWay);
        #endregion  

        #region FramePadding
        public Thickness FramePadding
        {
            get => (Thickness)base.GetValue(FramePaddingProperty);
            set => base.SetValue(FramePaddingProperty, value);
        }

        public static BindableProperty FramePaddingProperty = BindableProperty.Create(
            propertyName: "FramePadding",
            returnType: typeof(Thickness),
            declaringType: typeof(NavigationBar),
            defaultValue: new Thickness(5),
            defaultBindingMode: BindingMode.OneWay);
        #endregion

        #endregion

        #region LeftButton

        #region command
        public static readonly BindableProperty BackCommandProperty =
            BindableProperty.Create(nameof(BackCommand), typeof(ICommand), typeof(NavigationBar), null);

        public ICommand BackCommand
        {
            get { return (ICommand)GetValue(BackCommandProperty); }
            set { SetValue(BackCommandProperty, value); }
        }

        // Helper method for invoking commands safely
        public static void Execute(ICommand command)
        {
            if (command == null) return;
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }
        public Command BackOnTap => new Command(() => Execute(BackCommand));
        #endregion

        #region BackSymbol
        public string BackSymbol
        {
            get => base.GetValue(BackSymbolProperty)?.ToString();
            set => base.SetValue(BackSymbolProperty, value);
        }

        public static BindableProperty BackSymbolProperty = BindableProperty.Create(
            propertyName: "BackSymbol",
            returnType: typeof(string),
            declaringType: typeof(NavigationBar),
            defaultValue: "",
            defaultBindingMode: BindingMode.OneWay);

        #endregion

        #region HasLeftButton
        public bool HasLeftButton
        {
            get => (bool)base.GetValue(HasLeftButtonProperty);
            set => base.SetValue(HasLeftButtonProperty, value);
        }

        public static BindableProperty HasLeftButtonProperty = BindableProperty.Create(
            propertyName: "HasLeftButton",
            returnType: typeof(bool),
            declaringType: typeof(NavigationBar),
            defaultValue: true,
            defaultBindingMode: BindingMode.OneWay);

        #endregion

        #region LeftFontFamily
        public string LeftFontFamily
        {
            get => base.GetValue(LeftFontFamilyProperty)?.ToString();
            set => base.SetValue(LeftFontFamilyProperty, value);
        }

        public static BindableProperty LeftFontFamilyProperty = BindableProperty.Create(
            propertyName: "LeftFontFamily",
            returnType: typeof(string),
            declaringType: typeof(NavigationBar),
            defaultValue: "",
            defaultBindingMode: BindingMode.OneWay);

        #endregion      

        #region LeftFontSize
        public double LeftFontSize
        {
            get => (double)base.GetValue(LeftFontSizeProperty);
            set => base.SetValue(LeftFontSizeProperty, value);
        }

        public static BindableProperty LeftFontSizeProperty = BindableProperty.Create(
            propertyName: "LeftFontSize",
            returnType: typeof(double),
            declaringType: typeof(NavigationBar),
            defaultValue: 14.0,
            defaultBindingMode: BindingMode.OneWay);

        #endregion      

        #region LeftColor
        public Color LeftColor
        {
            get => (Color)base.GetValue(LeftColorProperty);
            set => base.SetValue(LeftColorProperty, value);
        }
        public static BindableProperty LeftColorProperty = BindableProperty.Create(
            propertyName: "LeftColor",
            returnType: typeof(Color),
            defaultValue: new Color(0, 0, 0),
            declaringType: typeof(NavigationBar),
            defaultBindingMode: BindingMode.OneWay);
        #endregion

        #endregion

        #region Title

        #region TitleColor
        public Color TitleColor
        {
            get => (Color)base.GetValue(TitleColorProperty);
            set => base.SetValue(TitleColorProperty, value);
        }
        public static BindableProperty TitleColorProperty = BindableProperty.Create(
            propertyName: "TitleColor",
            returnType: typeof(Color),
            defaultValue: new Color(0, 0, 0),
            declaringType: typeof(NavigationBar),
            defaultBindingMode: BindingMode.OneWay);
        #endregion

        #region TitleDisplay
        public string TitleDisplay
        {
            get => base.GetValue(TitleDisplayProperty)?.ToString();
            set => base.SetValue(TitleDisplayProperty, value);
        }

        public static BindableProperty TitleDisplayProperty = BindableProperty.Create(
            propertyName: "TitleDisplay",
            returnType: typeof(string),
            declaringType: typeof(NavigationBar),
            defaultValue: "",
            defaultBindingMode: BindingMode.OneWay);

        #endregion

        #region TitleFontSize
        public double TitleFontSize
        {
            get => (double)base.GetValue(TitleFontSizeProperty);
            set => base.SetValue(TitleFontSizeProperty, value);
        }

        public static BindableProperty TitleFontSizeProperty = BindableProperty.Create(
            propertyName: "TitleFontSize",
            returnType: typeof(double),
            declaringType: typeof(NavigationBar),
            defaultValue: 14.0,
            defaultBindingMode: BindingMode.OneWay);

        #endregion      

        #endregion
    }
}