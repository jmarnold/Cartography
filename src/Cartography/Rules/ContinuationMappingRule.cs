using Cartography.Runtime;
using FubuCore;

namespace Cartography.Rules
{
	public class ContinuationMappingRule : IMappingRule
	{
		public ContinuationMappingRule(PropertyMappingRequest request)
		{
			Request = request;
		}

		public PropertyMappingRequest Request { get; private set; }

		public object Map(IMappingContext context, object value)
		{
		    var provider = context.Get<IMappingProvider>();
			return provider.Map(Request.Source.PropertyType,
			                     Request.Destination.PropertyType, Request.Source.GetValue(value));
		}

        public override string ToString()
        {
            return "Continuation Mapping: {0}.{1} to {2}.{3}".ToFormat(Request.OwnerType.Name, Request.Source.Name,
                Request.Destination.OwnerType.Name, Request.Destination.Name);
        }
	}
}