using System.Windows.Input;
using Xamarin.Forms;

namespace Phoenix.Mobile.Controls
{

    public partial class CommonButton
    {
        public CommonButton()
        {
            InitializeComponent();
        }
        #region Command
        public static readonly BindableProperty CommandParameterProperty =
           BindableProperty.CreateAttached(
               propertyName: "CommandParameter",
               returnType: typeof(object),
               declaringType: typeof(CommonButton),
               defaultValue: null,
               defaultBindingMode: BindingMode.OneWay,
               validateValue: null);
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandProperty, value); }
        }
        // BindableProperty implementation
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CommonButton), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Helper method for invoking commands safely
        public static void Execute(ICommand command, object commandParameter)
        {
            if (command == null) return;
            if (command.CanExecute(commandParameter))
            {
                command.Execute(commandParameter);
            }
        }
        public Command OnTap => new Command(() => Execute(Command,CommandParameter));
        #endregion       
        
        #region Background

        public new Color Background
        {
            get => (Color)base.GetValue(BackgroundProperty);
            set => base.SetValue(BackgroundProperty, value);
        }

        public new static BindableProperty BackgroundProperty = BindableProperty.Create(
            propertyName: "Background",
            returnType: typeof(Color),
            defaultValue: new Color(1, 1, 1),
            declaringType: typeof(CommonButton),
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
            declaringType: typeof(CommonButton),
            defaultBindingMode: BindingMode.OneWay);

        #endregion
        #region ButtonPadding
        public Thickness ButtonPadding
        {
            get => (Thickness)base.GetValue(ButtonPaddingProperty);
            set => base.SetValue(ButtonPaddingProperty, value);
        }

        public static BindableProperty ButtonPaddingProperty = BindableProperty.Create(
            propertyName: "ButtonPadding",
            returnType: typeof(Thickness),
            declaringType: typeof(CommonButton),
            defaultValue: new Thickness(5),
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
            declaringType: typeof(CommonButton),
            defaultValue: 10,
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
            declaringType: typeof(CommonButton),
            defaultValue: false,
            defaultBindingMode: BindingMode.OneWay);
        #endregion

        #region HeightButton
        public double HeightButton
        {
            get => (double)base.GetValue(HeightButtonProperty);
            set => base.SetValue(HeightButtonProperty, value);
        }

        public static BindableProperty HeightButtonProperty = BindableProperty.Create(
            propertyName: "HeightButton",
            returnType: typeof(double),
            declaringType: typeof(CommonButton),
            defaultValue: 30.0,
            defaultBindingMode: BindingMode.OneWay);
        #endregion

        #region SubText area
        #region SubFontSize
        public double SubFontSize
        {
            get => (double)base.GetValue(SubFontSizeProperty);
            set => base.SetValue(SubFontSizeProperty, value);
        }

        public static BindableProperty SubFontSizeProperty = BindableProperty.Create(
            propertyName: "SubFontSize",
            returnType: typeof(double),
            declaringType: typeof(CommonButton),
            defaultValue: 14.0,
            defaultBindingMode: BindingMode.OneWay);
        #endregion
        #region SubTextColor

        public Color SubTextColor
        {
            get => (Color)base.GetValue(SubTextColorProperty);
            set => base.SetValue(SubTextColorProperty, value);
        }

        public static BindableProperty SubTextColorProperty = BindableProperty.Create(
            propertyName: "SubTextColor",
            returnType: typeof(Color),
            defaultValue: new Color(0, 0, 0),
            declaringType: typeof(CommonButton),
            defaultBindingMode: BindingMode.OneWay);

        #endregion
        #region SubText

        public string SubText
        {
            get => base.GetValue(SubTextProperty)?.ToString();
            set => base.SetValue(SubTextProperty, value);
        }

        public static BindableProperty SubTextProperty = BindableProperty.Create(
            propertyName: "SubText",
            returnType: typeof(string),
            declaringType: typeof(CommonButton),
            defaultValue: "",
            defaultBindingMode: BindingMode.OneWay);

        #endregion               
        #endregion

        #region HeaderText area
        #region FontSize
        public double FontSize
        {
            get => (double)base.GetValue(FontSizeProperty);
            set => base.SetValue(FontSizeProperty, value);
        }

        public static BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: "FontSize",
            returnType: typeof(double),
            declaringType: typeof(CommonButton),
            defaultValue: 20.0,
            defaultBindingMode: BindingMode.OneWay);
        #endregion
        #region Color

        public Color TextColor
        {
            get => (Color)base.GetValue(TextColorProperty);
            set => base.SetValue(TextColorProperty, value);
        }

        public static BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: "TextColor",
            returnType: typeof(Color),
            defaultValue: new Color(0, 0, 0),
            declaringType: typeof(CommonButton),
            defaultBindingMode: BindingMode.OneWay);

        #endregion
        #region Text
        public string Text
        {
            get => base.GetValue(TextProperty)?.ToString();
            set => base.SetValue(TextProperty, value);
        }

        public static BindableProperty TextProperty = BindableProperty.Create(
            propertyName: "Text",
            returnType: typeof(string),
            declaringType: typeof(CommonButton),
            defaultValue: "",
            defaultBindingMode: BindingMode.OneWay);

        #endregion
        #endregion

        #region IconStart area
        #region IconStartColor
        public Color IconStartColor
        {
            get => (Color)base.GetValue(IconStartColorProperty);
            set => base.SetValue(IconStartColorProperty, value);
        }
        public static BindableProperty IconStartColorProperty = BindableProperty.Create(
            propertyName: "IconStartColor",
            returnType: typeof(Color),
            defaultValue: new Color(0, 0, 0),
            declaringType: typeof(CommonButton),
            defaultBindingMode: BindingMode.OneWay);
        #endregion
        #region IconStartFontFamily
        public string IconStartFontFamily
        {
            get => base.GetValue(IconStartFontFamilyProperty)?.ToString();
            set => base.SetValue(IconStartFontFamilyProperty, value);
        }

        public static BindableProperty IconStartFontFamilyProperty = BindableProperty.Create(
            propertyName: "IconStartFontFamily",
            returnType: typeof(string),
            declaringType: typeof(CommonButton),
            defaultValue: "",
            defaultBindingMode: BindingMode.OneWay);

        #endregion      
        #region IconStartFontSize
        public double IconStartFontSize
        {
            get => (double)base.GetValue(IconStartFontSizeProperty);
            set => base.SetValue(IconStartFontSizeProperty, value);
        }

        public static BindableProperty IconStartFontSizeProperty = BindableProperty.Create(
            propertyName: "IconStartFontSize",
            returnType: typeof(double),
            declaringType: typeof(CommonButton),
            defaultValue: 14.0,
            defaultBindingMode: BindingMode.OneWay);

        #endregion      
        #region IconStart
        public string IconStart
        {
            get => base.GetValue(IconStartProperty)?.ToString();
            set => base.SetValue(IconStartProperty, value);
        }

        public static BindableProperty IconStartProperty = BindableProperty.Create(
            propertyName: "IconStart",
            returnType: typeof(string),
            declaringType: typeof(CommonButton),
            defaultValue: "",
            defaultBindingMode: BindingMode.OneWay);

        #endregion      
        #region HasIconStart
        public bool HasIconStart
        {
            get => (bool)base.GetValue(HasIconStartProperty);
            set => base.SetValue(HasIconStartProperty, value);
        }

        public static BindableProperty HasIconStartProperty = BindableProperty.Create(
            propertyName: "HasIconStart",
            returnType: typeof(bool),
            declaringType: typeof(CommonButton),
            defaultValue: true,
            defaultBindingMode: BindingMode.OneWay);

        #endregion

        #region IconStartWidth
        public int IconStartWidth
        {
            get => (int)base.GetValue(IconStartWidthProperty);
            set => base.SetValue(IconStartWidthProperty, value);
        }

        public static BindableProperty IconStartWidthProperty = BindableProperty.Create(
            propertyName: "IconStartWidth",
            returnType: typeof(int),
            declaringType: typeof(CommonButton),
            defaultValue: 10,
            defaultBindingMode: BindingMode.OneWay);
        #endregion        
        #endregion
    }
}