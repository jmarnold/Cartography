using System.Collections.Generic;
using Cartography.Rules;

namespace Cartography.Policies
{
	public interface IMappingPolicy
	{
		bool Matches(MappingRequest request);
		IEnumerable<IMappingRule> RulesFor(MappingRequest request);
	}
}