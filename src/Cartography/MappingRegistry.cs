using System.Collections.Generic;
using Cartography.Dsl;
using Cartography.Policies;
using Cartography.Runtime;

namespace Cartography
{
	public class MappingRegistry
	{
		private readonly IList<IMappingPolicy> _policies = new List<IMappingPolicy>();
		private readonly IList<IPropertyMappingPolicy> _propertyPolicies = new List<IPropertyMappingPolicy>();
		private readonly IList<IObjectResolver> _resolvers = new List<IObjectResolver>();
		private readonly IList<IObjectEnricher> _enrichers = new List<IObjectEnricher>();

		public ListModifierExpression<IMappingPolicy> Policies
		{
			get { return new ListModifierExpression<IMappingPolicy>(_policies); }
		}

		public ListModifierExpression<IPropertyMappingPolicy> PropertyPolicies
		{
			get { return new ListModifierExpression<IPropertyMappingPolicy>(_propertyPolicies); }
		}

		public ListModifierExpression<IObjectResolver> Resolvers
		{
			get { return new ListModifierExpression<IObjectResolver>(_resolvers); }
		}

		public ListModifierExpression<IObjectEnricher> Enrichers
		{
			get { return new ListModifierExpression<IObjectEnricher>(_enrichers); }
		}

		public MappingRegistry()
		{
			setupDefaults();
		}

		private void setupDefaults()
		{
			PropertyPolicies
				.Add<PrimitiveOrStringPropertyMappingPolicy>()
				.Add<ContinuationPropertyMappingPolicy>()
				.Add<EnumerablePropertyMappingPolicy>();

			Policies
				.Add(new MappingPolicySource(_propertyPolicies));

			Resolvers
				.Add<DefaultObjectResolver>();
		}

		public MappingConfigurationModel BuildConfiguration()
		{
			return new MappingConfigurationModel(_policies, _resolvers, _enrichers);
		}
	}
}