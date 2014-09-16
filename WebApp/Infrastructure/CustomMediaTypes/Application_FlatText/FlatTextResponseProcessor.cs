using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Responses.Negotiation;

namespace WebApp.Infrastructure.CustomMediaTypes.Application_FlatText
{
    public class FlatTextResponseProcessor : IResponseProcessor
    {
        public const string CustomContentType = "application/flat-text";
        public const string CustomContentExtension = "flat-text";
        private readonly ISerializer _serializer;

        public FlatTextResponseProcessor(IEnumerable<ISerializer> serializers)
        {
            _serializer = serializers.FirstOrDefault(x => x.CanSerialize(CustomContentType));
        }

        public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings
        {
            get { return new[] { new Tuple<string, MediaRange>(CustomContentExtension, new MediaRange(CustomContentType)) }; }
        }

        public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            if (requestedMediaRange.IsWildcard)
            {
                return new ProcessorMatch { ModelResult = MatchResult.NonExactMatch, RequestedContentTypeResult = MatchResult.NoMatch };
            }

            return !requestedMediaRange.Matches(CustomContentType) 
                ? new ProcessorMatch { ModelResult = MatchResult.NonExactMatch, RequestedContentTypeResult = MatchResult.NoMatch } 
                : new ProcessorMatch { ModelResult = MatchResult.ExactMatch, RequestedContentTypeResult = MatchResult.ExactMatch };
        }

        public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            return new Response
            {
                Contents = stream => _serializer.Serialize(CustomContentType, model, stream),
                ContentType = CustomContentType,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}