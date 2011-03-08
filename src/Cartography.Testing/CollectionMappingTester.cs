using System.Collections.Generic;
using Cartography.Policies;
using Cartography.Runtime;
using NUnit.Framework;

namespace Cartography.Testing
{
	[TestFixture]
	public class CollectionMappingTester
	{
		[Test]
		public void should_map_enumerable_properties()
		{
			IMappingProvider provider = null;
			var continuationPolicy = new ContinuationPropertyMappingPolicy();
			var enumerablePolicy = new EnumerablePropertyMappingPolicy();
			var propertyPolicy = new PrimitiveOrStringPropertyMappingPolicy();
			var mappingPolicy = new MappingPolicySource(new List<IPropertyMappingPolicy> { propertyPolicy, continuationPolicy, enumerablePolicy });
			var query = new MappingQuery(new List<IMappingPolicy> { mappingPolicy }, new List<IObjectResolver> { new DefaultObjectResolver() }, new List<IObjectEnricher>());
			provider = new MappingProvider(query, new MappingContext());

			var rootEntity = new SampleEntityWithCollection()
			                 	{
			                 		Id = 123,
			                 		Children = new List<SampleEntity>
			                 		           	{
													new SampleEntity
														{
															Id = 1,
															Name = "Child Entity 1"
														},
													new SampleEntity
														{
															Id = 2,
															Name = "Child Entity 2"
														}
			                 		           	}
			                 	};

			var model = provider.Map<SampleEntityWithCollection, SampleModelWithCollection>(rootEntity);

			model
				.Children
				.ShouldContain(m => m.Name == "Child Entity 1");
			model
				.Children
				.ShouldContain(m => m.Name == "Child Entity 2");
		}

		#region Nested Type: SampleEntity
		public class SampleEntity
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}
		#endregion

		#region Nested Type: SampleEntityWithCollection
		public class SampleEntityWithCollection
		{
			public int Id { get; set; }
			public IEnumerable<SampleEntity> Children { get; set; }
		}
		#endregion

		#region Nested Type: SampleModel
		public class SampleModel
		{
			public int Id { get; set; }
			public string Name { get; set; }

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != typeof(SampleEntity)) return false;
				return Equals((SampleEntity)obj);
			}

			public bool Equals(SampleEntity other)
			{
				if (ReferenceEquals(null, other)) return false;
				if (ReferenceEquals(this, other)) return true;
				return other.Id == Id && Equals(other.Name, Name);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					return (Id * 397) ^ (Name != null ? Name.GetHashCode() : 0);
				}
			}
		}
		#endregion

		#region Nested Type: SampleModelWithCollection
		public class SampleModelWithCollection
		{
			public int Id { get; set; }
			public IEnumerable<SampleModel> Children { get; set; }
		}
		#endregion
	}
}