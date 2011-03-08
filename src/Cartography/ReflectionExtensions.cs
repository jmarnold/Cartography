using System;
using System.Collections.Generic;
using System.Reflection;
using FubuCore;
using FubuCore.Reflection;

namespace Cartography
{
	public static class ReflectionExtensions
	{
		public static bool IsPrimitiveOrString(this Accessor accessor)
		{
			return accessor.PropertyType.IsPrimitive || accessor.PropertyType.Equals(typeof (string));
		}

		public static bool IsEnumerable(this Accessor accessor)
		{
			return accessor.PropertyType.IsEnumerable();
		}

		public static bool IsEnumerable(this Type type)
		{
			return type.Closes(typeof (IEnumerable<>)) && !type.Equals(typeof (string));
		}

		public static IEnumerable<PropertyInfo> PublicProperties(this Type type)
		{
			return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
		}

		public static void EachProperty(this Type type, Action<PropertyInfo> action)
		{
			type
				.PublicProperties()
				.Each(action);
		}

		public static object CloseAndBuildAs(this Type openType, params Type[] parameterTypes)
		{
			var typeDef = openType.MakeGenericType(parameterTypes);
			return Activator.CreateInstance(typeDef);
		}
	}
}