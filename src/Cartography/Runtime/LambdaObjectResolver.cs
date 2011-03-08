using System;

namespace Cartography.Runtime
{
	public class LambdaObjectResolver : IObjectResolver
	{
		private readonly Func<MappingRequest, bool> _predicate;
		private readonly Func<MappingRequest, object> _resolve;

		public LambdaObjectResolver(Func<MappingRequest, bool> predicate, Func<MappingRequest, object> resolve)
		{
			_predicate = predicate;
			_resolve = resolve;
		}

		public bool Matches(MappingRequest request)
		{
			return _predicate(request);
		}

		public object Resolve(MappingRequest request)
		{
			return _resolve(request);
		}
	}
}