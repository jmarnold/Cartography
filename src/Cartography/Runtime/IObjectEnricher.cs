using System;

namespace Cartography.Runtime
{
	public interface IObjectEnricher
	{
		bool Matches(Type type);
		void Enrich(object source, object destination);
	}
}