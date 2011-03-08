namespace Cartography.Runtime
{
	public interface IObjectResolver
	{
		bool Matches(MappingRequest request);
		object Resolve(MappingRequest request);
	}
}