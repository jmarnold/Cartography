using System.Collections.Generic;
using Cartography.Policies;
using NUnit.Framework;

namespace Cartography.Testing
{
	[TestFixture]
	public class MappingPolicySourceTester
	{
		private MappingPolicySource _policy;

		[SetUp]
		public void SetUp()
		{
			_policy = new MappingPolicySource(new List<IPropertyMappingPolicy> { new PrimitiveOrStringPropertyMappingPolicy() });
		}

		[Test]
		public void should_only_return_mapping_rules_for_requests_with_matching_policies()
		{
			var rules = _policy.RulesFor(new MappingRequest(typeof (SampleModel), typeof (SampleModel)));

			rules.ShouldHaveCount(2);
			rules.ShouldContain(r => r.Request.Source.Name == "StringProperty");
			rules.ShouldContain(r => r.Request.Source.Name == "IntProperty");
		}
	}
}