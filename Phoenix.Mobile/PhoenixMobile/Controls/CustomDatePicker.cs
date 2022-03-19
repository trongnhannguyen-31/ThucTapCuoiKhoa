using Phoenix.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Phoenix.Mobile.Controls
{
    public class CustomDatePicker : BorderlessDatePicker
    {
        private string _format = null;
        [Obsolete]
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create<CustomDatePicker, DateTime?>(p => p.NullableDate, null);

        [Obsolete]
        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set { SetValue(NullableDateProperty, value); UpdateDate(); }
        }

        [Obsolete]
        private void UpdateDate()
        {
            if (NullableDate.HasValue) { if (null != _format) Format = _format; Date = NullableDate.Value; }
            else { _format = Format; Format = "--/--/----".PadLeft(7, ' '); }
        }

        [Obsolete]
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        [Obsolete]
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(base.Date)) NullableDate = Date;
        }
    }
}
