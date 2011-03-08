using System;
using System.Collections.Generic;
using System.Linq;
using Cartography.Policies;
using Cartography.Runtime;

namespace Cartography
{
	public class MappingQuery : IMappingQuery
	{
		private readonly IEnumerable<IMappingPolicy> _policies;
		private readonly IEnumerable<IObjectResolver> _resolvers;
		private readonly IEnumerable<IObjectEnricher> _enrichers;

		public MappingQuery(IEnumerable<IMappingPolicy> policies, IEnumerable<IObjectResolver> resolvers,
		                    IEnumerable<IObjectEnricher> enrichers)
		{
			_policies = policies;
			_enrichers = enrichers;
			_resolvers = resolvers;
		}

		public MappingResult MapFor(Type sourceType, Type destinationType)
		{
			return MapFor(new MappingRequest(sourceType, destinationType));
		}

		public MappingResult MapFor(MappingRequest request)
		{
			var rules = _policies
				.Where(p => p.Matches(request))
				.SelectMany(p => p.RulesFor(request));

			return new MappingResult(request.SourceType, request.DestinationType, rules, _resolvers, _enrichers);
		}
	}
}