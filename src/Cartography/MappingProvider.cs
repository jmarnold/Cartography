using System;
using System.Collections.Generic;
using System.Linq;
using Cartography.Runtime;

namespace Cartography
{
	public class MappingProvider : IMappingProvider
	{
		private readonly IMappingQuery _query;
	    private readonly IMappingContext _context;

		public MappingProvider(IMappingQuery query, IMappingContext context)
		{
		    _query = query;
		    _context = context;
		}

	    public TDestination Map<TOrigin, TDestination>(TOrigin origin)
		{
			return (TDestination) Map(typeof (TOrigin), typeof (TDestination), origin);
		}

		public object Map(Type sourceType, Type destinationType, object source)
		{
			var request = new MappingRequest(sourceType, destinationType);
			var result = _query.MapFor(request);
			var resolver = result.Resolvers.LastOrDefault(r => r.Matches(request));
			if(resolver == null)
			{
				return null;
			}

            if(!_context.Has<IMappingProvider>())
            {
                _context.Set<IMappingProvider>(this);
            }
            
			var destination = resolver.Resolve(request);

			result
				.EachRule(rule =>
				          	{
				          		var destinationValue = rule.Map(_context, source);
								if(destinationValue == null)
								{
									return;
								}

				          		rule
                                    .Request
                                    .Destination
                                    .SetValue(destination, destinationValue);
				          	});

			result
				.Enrichers
				.Where(e => e.Matches(destinationType))
				.Each(e => e.Enrich(source, destination));

			return destination;
		}
	}
}