using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cartography.Rules;
using Cartography.Runtime;
using FubuCore.Util;

namespace Cartography.Policies
{
	public class MappingPolicySource : IMappingPolicy
	{
		private readonly IEnumerable<IPropertyMappingPolicy> _policies;
		private readonly Cache<MappingRequest, IEnumerable<IMappingRule>> _rules;

		public MappingPolicySource(IEnumerable<IPropertyMappingPolicy> policies)
		{
			_policies = policies;
			_rules = new Cache<MappingRequest, IEnumerable<IMappingRule>>(BuildRules);
		}

		public bool Matches(MappingRequest request)
		{
			return true;
		}

		public IEnumerable<IMappingRule> BuildRules(MappingRequest request)
		{
			var sourceProps = request
				.SourceType
				.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			var destinationProps = request
				.DestinationType
				.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			var rules = new List<IMappingRule>();
			foreach (var sourceProperty in sourceProps)
			{
				foreach (var destinationProperty in destinationProps)
				{
					var propertyRequest = PropertyMappingRequest.For(request.SourceType, sourceProperty, destinationProperty);
					var policy = _policies.LastOrDefault(p => p.Matches(propertyRequest));
					if (policy == null)
					{
						continue;
					}

					rules.Add(policy.RuleFor(propertyRequest));
				}
			}

			return rules;
		}

		public IEnumerable<IMappingRule> RulesFor(MappingRequest request)
		{
			return _rules[request];
		}
	}
}