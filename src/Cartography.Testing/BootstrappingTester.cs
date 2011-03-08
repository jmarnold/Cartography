using System.Linq;
using Cartography.Runtime;
using NUnit.Framework;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Cartography.Testing
{
    [TestFixture]
    public class BootstrappingTester
    {
        private IContainer _container;

        [SetUp]
        public void SetUp()
        {
            _container = new Container(new ModelMappingRegistry());
        }

        [Test]
        public void should_register_provider_with_container()
        {
            _container
                .Model
                .DefaultTypeFor<IMappingProvider>()
                .ShouldEqual(typeof (MappingProvider));
        }

        [Test]
        public void should_register_query_with_container()
        {
            _container
                .Model
                .InstancesOf<IMappingQuery>()
                .SingleOrDefault()
                .ShouldNotBeNull();
        }

        [Test]
        public void should_register_context_with_container()
        {
            _container
                .Model
                .InstancesOf<IMappingContext>()
                .SingleOrDefault()
                .ShouldNotBeNull();
            
        }

        public class ModelMappingRegistry : Registry
        {
            public ModelMappingRegistry()
            {
                this.ConfigureMapping<MappingRegistry>();
            }
        }
    }


}