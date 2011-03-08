using System;

namespace Cartography
{
	public interface IMappingProvider
	{
		TDestination Map<TOrigin, TDestination>(TOrigin origin);
		object Map(Type sourceType, Type destinationType, object source);
	}
}