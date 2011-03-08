using System;

namespace Cartography.Runtime
{
	public class DefaultObjectResolver : IObjectResolver
	{
		public bool Matches(MappingRequest request)
		{
			return true;
		}

		public object Resolve(MappingRequest request)
		{
			return Activator.CreateInstance(request.DestinationType);
		}
	}
}