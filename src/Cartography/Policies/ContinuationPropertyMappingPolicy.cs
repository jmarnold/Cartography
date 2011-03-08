using Cartography.Rules;
using Cartography.Runtime;

namespace Cartography.Policies
{
	public class ContinuationPropertyMappingPolicy : IPropertyMappingPolicy
	{
		public bool Matches(PropertyMappingRequest request)
		{
			return !request.Source.IsPrimitiveOrString()
			       && !request.Destination.IsPrimitiveOrString() && request.NamesMatch();
		}

		public IMappingRule RuleFor(PropertyMappingRequest request)
		{
			return new ContinuationMappingRule(request);
		}
	}
}