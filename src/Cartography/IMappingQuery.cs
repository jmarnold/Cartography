using System;

namespace Cartography
{
	public interface IMappingQuery
	{
		MappingResult MapFor(MappingRequest request);
		MappingResult MapFor(Type sourceType, Type destinationType);
	}
}