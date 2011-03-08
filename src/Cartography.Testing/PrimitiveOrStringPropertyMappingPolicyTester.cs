using Cartography.Policies;
using Cartography.Runtime;
using FubuCore.Reflection;
using NUnit.Framework;

namespace Cartography.Testing
{
	[TestFixture]
	public class PrimitiveOrStringPropertyMappingPolicyTester
	{
		[Test]
		public void should_not_match_requests_for_reference_types_that_are_not_strings()
		{
			var request = new PropertyMappingRequest(typeof (SampleModel),
			                                 ReflectionHelper.GetAccessor<SampleModel>(m => m.Parent),
			                                 ReflectionHelper.GetAccessor<SampleModel>(m => m.Parent));

			new PrimitiveOrStringPropertyMappingPolicy()
				.Matches(request)
				.ShouldBeFalse();
		}

		[Test]
		public void should_match_requests_for_primitives_or_strings()
		{
			var request = new PropertyMappingRequest(typeof(SampleModel),
			                                 ReflectionHelper.GetAccessor<SampleModel>(m => m.StringProperty),
			                                 ReflectionHelper.GetAccessor<SampleModel>(m => m.StringProperty));

			new PrimitiveOrStringPropertyMappingPolicy()
				.Matches(request)
				.ShouldBeTrue();
		}

		[Test]
		public void should_not_match_requests_for_properties_whose_types_do_not_match()
		{
			var request = new PropertyMappingRequest(typeof(SampleModel),
			                                 ReflectionHelper.GetAccessor<SampleModel>(m => m.StringProperty),
			                                 ReflectionHelper.GetAccessor<SampleModel>(m => m.IntProperty));

			new PrimitiveOrStringPropertyMappingPolicy()
				.Matches(request)
				.ShouldBeFalse();
		}

		[Test]
		public void should_map_value()
		{
			var request = new PropertyMappingRequest(typeof(SampleModel),
											 ReflectionHelper.GetAccessor<SampleModel>(m => m.StringProperty),
											 ReflectionHelper.GetAccessor<SampleModel>(m => m.StringProperty));

			var model = new SampleModel { StringProperty = "Test" };
			new PrimitiveOrStringPropertyMappingPolicy()
				.RuleFor(request)
				.Map(new MappingContext((IMappingProvider)null), model)
				.ShouldEqual(model.StringProperty);
		}
	}
}