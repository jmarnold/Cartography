using Cartography.Rules;
using Cartography.Runtime;

namespace Cartography.Policies
{
	public interface IPropertyMappingPolicy
	{
		bool Matches(PropertyMappingRequest request);
		IMappingRule RuleFor(PropertyMappingRequest request);
	}
}