using System;
using System.Linq;

namespace Domain.Extensions
{
	public static class DataAttribute
	{
		public static T GetPropertyAttribute<T>(this object instance, string propertyName) where T : Attribute
		{
			return (T)instance.GetType()
				.GetProperty(propertyName)
				.GetCustomAttributes(typeof(T), false)
				.First();
		}

		public static T GetFieldAttribute<T>(this Enum enumField) where T : Attribute
		{
			return (T) enumField.GetType()
				.GetField(enumField.ToString())
				.GetCustomAttributes(typeof(T), false)
				.First();
		}
	}
}
