using Cartography.Rules;
using Cartography.Runtime;

namespace Cartography.Policies
{
	public class EnumerablePropertyMappingPolicy : IPropertyMappingPolicy
	{
		public bool Matches(PropertyMappingRequest request)
		{
            return request.Source.IsEnumerable() && request.Destination.IsEnumerable() && request.NamesMatch();
		}

		public IMappingRule RuleFor(PropertyMappingRequest request)
		{
			return new EnumerableMappingRule(request);
		}
	}
}