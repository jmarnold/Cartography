using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cartography.Runtime;
using FubuCore;
using FubuCore.Reflection;

namespace Cartography.Rules
{
	public class EnumerableMappingRule : IMappingRule
	{
		public EnumerableMappingRule(PropertyMappingRequest request)
		{
			Request = request;
		}

		public PropertyMappingRequest Request { get; private set; }

		public object Map(IMappingContext context, object value)
		{
			var sourceValues = Request.Source.GetValue(value) as IEnumerable;
			var sourceType = getTargetType(Request.Source);
			var destinationType = getTargetType(Request.Destination);

			if (sourceType == null || destinationType == null || sourceValues == null)
			{
				// TODO -- logging??
				return null;
			}

			var provider = context.Get<IMappingProvider>();
			var destinationValues = (IList) typeof (List<>).CloseAndBuildAs(destinationType);
			sourceValues.Each(src => destinationValues.Add(provider.Map(sourceType, destinationType, src)));
			return destinationValues;
		}

		private Type getTargetType(Accessor accessor)
		{
			var enumerable = accessor.PropertyType.FindInterfaceThatCloses(typeof (IEnumerable<>));
			if (enumerable == null)
			{
				return null;
			}

			return enumerable
				.GetGenericArguments()
				.First();
		}

		public override string ToString()
		{
			return "Enumerable Mapping: {0}.{1} to {2}.{3}".ToFormat(Request.OwnerType.Name, Request.Source.Name,
			                                                         Request.Destination.OwnerType.Name, Request.Destination.Name);
		}
	}
}