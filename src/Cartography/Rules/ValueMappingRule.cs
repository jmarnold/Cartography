using System;
using Cartography.Runtime;
using FubuCore;

namespace Cartography.Rules
{
	public class ValueMappingRule : IMappingRule
	{
		private readonly Func<object, object> _map;

		public ValueMappingRule(PropertyMappingRequest request, Func<object, object> map)
		{
			Request = request;
			_map = map;
		}

		public PropertyMappingRequest Request { get; private set; }

		public object Map(IMappingContext context, object value)
		{
			return _map(value);
		}

        public override string ToString()
        {
            return "Value Mapping: {0}.{1} to {2}.{3}".ToFormat(Request.OwnerType.Name, Request.Source.Name,
                Request.Destination.OwnerType.Name, Request.Destination.Name);
        }
	}
}