using Cartography.Runtime;

namespace Cartography.Rules
{
	public interface IMappingRule
	{
		PropertyMappingRequest Request { get; }
		object Map(IMappingContext context, object value);
	}
}