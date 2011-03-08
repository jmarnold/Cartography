using Cartography.Runtime;
using StructureMap.Configuration.DSL;

namespace Cartography
{
    public static class StructureMapExtensions
    {
        public static void ConfigureMapping<TRegistry>(this Registry registry)
            where TRegistry : MappingRegistry, new()
        {
            var query = new TRegistry().BuildQuery();
            
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