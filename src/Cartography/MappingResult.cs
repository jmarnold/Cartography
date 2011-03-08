using System;
using System.Collections.Generic;
using Cartography.Rules;
using Cartography.Runtime;

namespace Cartography
{
	public class MappingResult
	{
		public MappingResult(Type sourceType, Type destinationType, IEnumerable<IMappingRule> rules,
		                     IEnumerable<IObjectResolver> resolvers, IEnumerable<IObjectEnricher> enrichers)
		{
			SourceType = sourceType;
			DestinationType = destinationType;
			Rules = rules;
			Resolvers = resolvers;
			Enrichers = enrichers;
		}

		public Type SourceType { get; private set; }
		public Type DestinationType { get; private set; }
		public IEnumerable<IMappingRule> Rules { get; private set; }
		public IEnumerable<IObjectResolver> Resolvers { get; private set; }
		public IEnumerable<IObjectEnricher> Enrichers { get; private set; }

		public void EachRule(Action<IMappingRule> action)
		{
			Rules.Each(action);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (MappingResult)) return false;
			return Equals((MappingResult) obj);
		}

		public bool Equals(MappingResult other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.SourceType, SourceType) && Equals(other.DestinationType, DestinationType);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((SourceType != null ? SourceType.GetHashCode() : 0)*397) ^
				       (DestinationType != null ? DestinationType.GetHashCode() : 0);
			}
		}
	}
}