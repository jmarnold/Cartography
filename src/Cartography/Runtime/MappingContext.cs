using System;
using FubuCore.Util;

namespace Cartography.Runtime
{
	public class MappingContext : IMappingContext
	{
		private readonly Cache<Type, object> _values;

		public MappingContext(IMappingProvider provider)
			: this(type => null)
		{
			Set(provider);
		}

		public MappingContext(Func<Type, object> onMissing)
		{
			_values = new Cache<Type, object>(onMissing);
		}

		public object Get(Type type)
		{
			return _values[type];
		}

		public T Get<T>() where T : class
		{
			return (T) Get(typeof (T));
		}

		public void Set<T>(T value) where T : class
		{
			Set(typeof (T), value);
		}

		public void Set(Type type, object value)
		{
			_values.Fill(type, value);
		}

		public bool Has<T>() where T : class
		{
			return Has(typeof (T));
		}

		public bool Has(Type type)
		{
			return _values.Has(type);
		}
	}
}