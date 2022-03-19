using System;
using System.ComponentModel;
using Android.Views.InputMethods;
using Phoenix.Framework.Controls;

namespace Phoenix.Droid.Controls
{
	public static class EnumExtensions
	{
		public static ImeAction GetValueFromDescription(this ReturnKeyTypes value)
		{
			var type = typeof(ImeAction);
			if (!type.IsEnum) throw new InvalidOperationException();
			foreach (var field in type.GetFields())
			{
				if (Attribute.GetCustomAttribute(field,
					typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
				{
					if (attribute.Description == value.ToString())
						return (ImeAction)field.GetValue(null);
				}
				else
				{
					if (field.Name == value.ToString())
						return (ImeAction)field.GetValue(null);
				}
			}
			throw new NotSupportedException($"Not supported on Android: {value}");
		}
	}
}