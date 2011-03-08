using Cartography.Runtime;
using StructureMap.Configuration.DSL;

namespace Cartography.Testing
{
	public static class StructureMapExtensions
	{
		public static void ConfigureMapping<TRegistry>(this Registry registry)
			where TRegistry : MappingRegistry, new()
		{
			var model = new TRegistry().BuildConfiguration();
			var query = new MappingQuery(model.Policies, model.Resolvers, model.Enrichers);

			registry
				.For<IMappingQuery>()
				.Use(query);

			registry
				.For<IMappingProvider>()
				.Use<MappingProvider>();

			registry
				.For<IMappingContext>()
				.Use(ctx => new MappingContext(ctx.GetInstance));
		}
	}
}