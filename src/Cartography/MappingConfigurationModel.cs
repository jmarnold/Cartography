using System.Collections.Generic;
using Cartography.Policies;
using Cartography.Runtime;

namespace Cartography
{
	public class MappingConfigurationModel
	{
		private readonly IEnumerable<IMappingPolicy> _policies;
		private readonly IEnumerable<IObjectResolver> _resolvers;
		private readonly IEnumerable<IObjectEnricher> _enrichers;

		public MappingConfigurationModel(IEnumerable<IMappingPolicy> policies, IEnumerable<IObjectResolver> resolvers,
		                                 IEnumerable<IObjectEnricher> enrichers)
		{
			_policies = policies;
			_enrichers = enrichers;
			_resolvers = resolvers;
		}

		public IEnumerable<IObjectEnricher> Enrichers
		{
			get { return _enrichers; }
		}

		public IEnumerable<IObjectResolver> Resolvers
		{
			get { return _resolvers; }
		}

		public IEnumerable<IMappingPolicy> Policies
		{
			get { return _policies; }
		}
	}
}