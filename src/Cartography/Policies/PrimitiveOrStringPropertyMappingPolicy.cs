using Cartography.Rules;
using Cartography.Runtime;

namespace Cartography.Policies
{
	public class PrimitiveOrStringPropertyMappingPolicy : IPropertyMappingPolicy
	{
		public bool Matches(PropertyMappingRequest request)
		{
			return request.Source.IsPrimitiveOrString() && request.Destination.IsPrimitiveOrString()
			       && request.Source.PropertyType.Equals(request.Destination.PropertyType)
			       && request.NamesMatch();
		}

		public IMappingRule RuleFor(PropertyMappingRequest request)
		{
			return new ValueMappingRule(request, source => request.Source.GetValue(source));
		}
	}
}