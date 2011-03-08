using System;

namespace Cartography.Runtime
{
	public class LambdaObjectEnricher : IObjectEnricher
	{
		private readonly Func<Type, bool> _predicate;
		private readonly Action<object, object> _enrich;

		public LambdaObjectEnricher(Func<Type, bool> predicate, Action<object, object> enrich)
		{
			_predicate = predicate;
			_enrich = enrich;
		}

		public bool Matches(Type type)
		{
			return _predicate(type);
		}

		public void Enrich(object source, object destination)
		{
			_enrich(source, destination);
		}
	}

	public class LambdaObjectEnricher<TSource, TDestination> : IObjectEnricher
		where TSource : class
		where TDestination : class
	{
		private readonly Action<TSource, TDestination> _enrich;

		public LambdaObjectEnricher(Action<TSource, TDestination> enrich)
		{
			_enrich = enrich;
		}

		public bool Matches(Type type)
		{
			return typeof (TDestination).Equals(type);
		}

		public void Enrich(object source, object destination)
		{
			_enrich((TSource)source, (TDestination)destination);
		}
	}
}