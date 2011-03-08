using System.Collections.Generic;
using Cartography;
using Cartography.Policies;
using Cartography.Runtime;
using FubuMVC.Core;

namespace FubuMVC.Cartography
{
	public static class FubuRegistryExtensions
	{
		public static void Mapping<TRegistry>(this FubuRegistry registry)
			where TRegistry : MappingRegistry, new()
		{
			registry.Mapping(new TRegistry());
		}

		public static void Mapping(this FubuRegistry registry, MappingRegistry mappingRegistry)
		{
			// Don't override existing configuration
			if (registry.IsMappingConfigured())
			{
				return;
			}

			var configuration = mappingRegistry.BuildConfiguration();
			registry
				.Services(x =>
				          	{
				          		configuration
				          			.Resolvers
				          			.Each(x.AddService<IObjectResolver>);

				          		configuration
				          			.Policies
				          			.Each(x.AddService<IMappingPolicy>);

				          		configuration
				          			.Enrichers
				          			.Each(x.AddService<IObjectEnricher>);

								x.SetServiceIfNone<IMappingQuery, MappingQuery>();
								x.SetServiceIfNone<IMappingProvider, MappingProvider>();
								x.SetServiceIfNone<IMappingContext, MappingContext>();
				          	});
		}

		public static bool IsMappingConfigured(this FubuRegistry registry)
		{
			// TODO -- There's GOT to be a cleaner way to do this
			var configured = false;
			registry
				.Services(x =>
				{
					configured = x.DefaultServiceFor<IMappingProvider>() != null;
				});

			return configured;
		}
	}
}