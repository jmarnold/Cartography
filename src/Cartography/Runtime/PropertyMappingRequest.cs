using System;
using System.Reflection;
using FubuCore;
using FubuCore.Reflection;

namespace Cartography.Runtime
{
	public class PropertyMappingRequest
	{
		public PropertyMappingRequest(Type ownerType, Accessor source, Accessor destination)
		{
			OwnerType = ownerType;
			Source = source;
			Destination = destination;
		}

		public Type OwnerType { get; private set; }
		public Accessor Source { get; private set; }
		public Accessor Destination { get; private set; }

        public bool NamesMatch()
        {
            return Source.Name.Equals(Destination.Name, StringComparison.InvariantCultureIgnoreCase);
        }

		public static PropertyMappingRequest For(Type ownerType, PropertyInfo sourceProperty, PropertyInfo destinationProperty)
		{
			return new PropertyMappingRequest(ownerType, new SingleProperty(sourceProperty), new SingleProperty(destinationProperty));
		}

        public override string ToString()
        {
            return "Property Request: {0}.{1} to {2}.{3}".ToFormat(OwnerType.Name, Source.Name, Destination.OwnerType.Name, Destination.Name);
        }
	}
}