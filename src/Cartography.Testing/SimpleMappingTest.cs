using System.Collections.Generic;
using Cartography.Policies;
using Cartography.Runtime;
using NUnit.Framework;

namespace Cartography.Testing
{
	[TestFixture]
	public class SimpleMappingTest
	{
		[Test]
		public void should_map_simple_properties()
		{
			var propertyPolicy = new PrimitiveOrStringPropertyMappingPolicy();
			var mappingPolicy = new MappingPolicySource(new List<IPropertyMappingPolicy> { propertyPolicy });
			var query = new MappingQuery(new List<IMappingPolicy> { mappingPolicy }, new List<IObjectResolver> { new DefaultObjectResolver() }, new List<IObjectEnricher>());
			var provider = new MappingProvider(query, new MappingContext());

			var origin = new EasyModel
			            	{
			            		Id = 123,
			            		FirstName = "Josh",
			            		LastName = "Arnold"
			            	};

			provider
				.Map<EasyModel, EasyModel>(origin)
				.ShouldEqual(origin);
		}

		[Test]
		public void should_map_complex_properties()
		{
			IMappingProvider provider = null;
			var continuationPolicy = new ContinuationPropertyMappingPolicy();
			var propertyPolicy = new PrimitiveOrStringPropertyMappingPolicy();
			var mappingPolicy = new MappingPolicySource(new List<IPropertyMappingPolicy> { propertyPolicy, continuationPolicy });
			var query = new MappingQuery(new List<IMappingPolicy> { mappingPolicy }, new List<IObjectResolver> { new DefaultObjectResolver() }, new List<IObjectEnricher>());
			provider = new MappingProvider(query, new MappingContext());

			var child = new EasyModel
			             	{
			             		Id = 123,
			             		FirstName = "Josh",
			             		LastName = "Arnold"
			             	};

			var origin = new ComplexModel {Id = 1234, Child = child};

			provider
				.Map<ComplexModel, ComplexModel>(origin)
				.ShouldEqual(origin);
		}

		public class ComplexModel
		{
			public int Id { get; set; }
			public EasyModel Child { get; set; }

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != typeof (ComplexModel)) return false;
				return Equals((ComplexModel) obj);
			}

			public bool Equals(ComplexModel other)
			{
				if (ReferenceEquals(null, other)) return false;
				if (ReferenceEquals(this, other)) return true;
				return other.Id == Id && Equals(other.Child, Child);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					return (Id*397) ^ (Child != null ? Child.GetHashCode() : 0);
				}
			}
		}

		public class EasyModel
		{
			public int Id { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != typeof (EasyModel)) return false;
				return Equals((EasyModel) obj);
			}

			public bool Equals(EasyModel other)
			{
				if (ReferenceEquals(null, other)) return false;
				if (ReferenceEquals(this, other)) return true;
				return other.Id == Id && Equals(other.FirstName, FirstName) && Equals(other.LastName, LastName);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					int result = Id;
					result = (result*397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
					result = (result*397) ^ (LastName != null ? LastName.GetHashCode() : 0);
					return result;
				}
			}
		}
	}
}