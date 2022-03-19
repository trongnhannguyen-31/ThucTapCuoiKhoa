using System.Linq;
using Xamarin.Forms;

namespace Phoenix.Mobile.Controls
{
    public class AnimatedText : StackLayout
    {
        private const string AnimationName = "AnimatedTextAnimation";

        public static readonly BindableProperty IsRunningProperty =
            BindableProperty.Create(nameof(IsRunning), typeof(bool),
                typeof(AnimatedText), default(bool));

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string),
                typeof(AnimatedText),
                default(string));

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double),
                typeof(AnimatedText),
                default(double));

        public static readonly BindableProperty FontAttributeProperty =
            BindableProperty.Create(nameof(FontAttribute), typeof(FontAttributes),
                typeof(AnimatedText),
                FontAttributes.None);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color),
                typeof(AnimatedText),
                Color.Black);

        private Animation _animation;

        public AnimatedText()
        {
            Orientation = StackOrientation.Horizontal;
            Spacing = -1;
        }

        public bool IsRunning
        {
            get => (bool)GetValue(IsRunningProperty);
            set => SetValue(IsRunningProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }
        public FontAttributes FontAttribute
        {
            get => (FontAttributes)GetValue(FontAttributeProperty);
            set => SetValue(FontAttributeProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName)
            {
                case nameof(Text):
                    InitAnimation();
                    break;
                case nameof(IsRunning) when IsEnabled:
                    {
                        if (IsRunning)
                            StartAnimation();
                        else
                            StopAnimation();
                        break;
                    }
            }
        }

        private void InitAnimation()
        {
            _animation = new Animation();
            Children.Clear();
            if (string.IsNullOrWhiteSpace(Text))
                return;

            var index = 0;
            foreach (var label in Text.Select(textChar => new Label
            {
                Text = textChar.ToString(),
                FontAttributes = FontAttribute,
                FontSize = FontSize,
                TextColor = TextColor,
            }))
            {
                Children.Add(label);
                if (string.IsNullOrEmpty(label.Text)) continue;
                var oneCharAnimationLength = (double)1 / (Text.Length + 1);

                _animation.Add(index * oneCharAnimationLength, (index + 1) * oneCharAnimationLength,
                    new Animation(v => label.Scale = v, 1, 1.2, Easing.Linear));
                _animation.Add((index + 1) * oneCharAnimationLength, (index + 2) * oneCharAnimationLength,
                    new Animation(v => label.Scale = v, 1.2, 1, Easing.Linear));

                _animation.Add(index * oneCharAnimationLength, (index + 1) * oneCharAnimationLength,
                    new Animation(v => label.TranslationY = v, 0, -10, Easing.Linear));
                _animation.Add((index + 1) * oneCharAnimationLength, (index + 2) * oneCharAnimationLength,
                    new Animation(v => label.TranslationY = v, -10, 0, Easing.Linear));

                index++;
            }
        }

        private void StartAnimation()
        {
            if (Children != null)
            {
                _animation?.Commit(this, AnimationName, 16,
                    (uint)Children.OfType<Label>().Count(x => !string.IsNullOrEmpty(x.Text)) * 200,
                    Easing.Linear,
                    null, () => true);
            }
        }

        private void StopAnimation()
        {
            this.AbortAnimation(AnimationName);
        }
    }
}
