using System;

namespace Cartography
{
	public class MappingRequest
	{
		public MappingRequest(Type sourceType, Type destinationType)
		{
			SourceType = sourceType;
			DestinationType = destinationType;
		}

		public Type SourceType { get; private set; }
		public Type DestinationType { get; private set; }

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (MappingRequest)) return false;
			return Equals((MappingRequest) obj);
		}

		public bool Equals(MappingRequest other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.SourceType, SourceType) && Equals(other.DestinationType, DestinationType);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((SourceType != null ? SourceType.GetHashCode() : 0)*397) ^ (DestinationType != null ? DestinationType.GetHashCode() : 0);
			}
		}
	}
}