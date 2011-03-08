using System;
using System.Collections.Generic;
using Cartography.Policies;
using Cartography.Runtime;
using NUnit.Framework;

namespace Cartography.Testing
{
	[TestFixture]
	public class ObjectResolutionAndEnrichmentTester
	{
		[Test]
		public void should_construct_with_resolver_and_enrich()
		{
			IMappingProvider provider = null;
			var continuationPolicy = new ContinuationPropertyMappingPolicy();
			var enumerablePolicy = new EnumerablePropertyMappingPolicy();
			var propertyPolicy = new PrimitiveOrStringPropertyMappingPolicy();
			var mappingPolicy = new MappingPolicySource(new List<IPropertyMappingPolicy> { propertyPolicy, continuationPolicy, enumerablePolicy });
			var resolver = new LambdaObjectResolver(r => r.DestinationType.Equals(typeof (Notification)), r => new Notification(r.SourceType));
			var enricher = new LambdaObjectEnricher<ModelWithErrors, Notification>((src, dest) => src
			                                                                                        	.CustomErrors
			                                                                                        	.Each(dest.RegisterError));
			var query = new MappingQuery(new List<IMappingPolicy> { mappingPolicy }, new List<IObjectResolver> { resolver }, new List<IObjectEnricher> { enricher });
			provider = new MappingProvider(query, new MappingContext());

			var model = new ModelWithErrors {CustomErrors = new List<string> {"Error 1", "Error 2"}};
			var notification = provider.Map<ModelWithErrors, Notification>(model);

			notification
				.Type
				.ShouldEqual(typeof (ModelWithErrors));

			notification
				.Messages
				.ShouldContain(m => m.Message == "Error 1");

			notification
				.Messages
				.ShouldContain(m => m.Message == "Error 2");
		}

		public class ModelWithErrors
		{
			public IEnumerable<string> CustomErrors { get; set; }
		}

		public class Notification
		{
			private readonly IList<NotificationMessage> _messages;
			public Notification(Type type)
			{
				Type = type;
				_messages = new List<NotificationMessage>();
			}

			public Type Type { get; set; }
			public IEnumerable<NotificationMessage> Messages { get { return _messages; } }

			public void RegisterError(string error)
			{
				_messages.Fill(new NotificationMessage { Message = error });
			}
		}

		public class NotificationMessage
		{
			public string Message { get; set; }
		}
	}
}