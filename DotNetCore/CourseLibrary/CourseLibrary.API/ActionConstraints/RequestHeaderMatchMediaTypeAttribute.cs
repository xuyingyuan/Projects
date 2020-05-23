using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.ActionConstraints
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class RequestHeaderMatchMediaTypeAttribute : Attribute, IActionConstraint
    {
        private readonly MediaTypeCollection _mediaTypes = new MediaTypeCollection();
        private readonly string _requestHeaderToMatch;

        public RequestHeaderMatchMediaTypeAttribute(string requestHeaderToMatch, string mediaType, params string[] otherMediaTypes)
        {
            _requestHeaderToMatch = requestHeaderToMatch ?? throw new ArgumentNullException(nameof(requestHeaderToMatch));

            if(MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                _mediaTypes.Add(parsedMediaType);
            }
            else
            {
                throw new ArgumentException(nameof(mediaType));
            }

            foreach(var othermediaType in otherMediaTypes)
            {
                if (MediaTypeHeaderValue.TryParse(othermediaType, out MediaTypeHeaderValue parsedOtherMediaType))
                {
                    _mediaTypes.Add(parsedOtherMediaType);
                }
                else
                {
                    throw new ArgumentException(nameof(othermediaType));
                }
            }
        }
        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            var requstHeaders = context.RouteContext.HttpContext.Request.Headers;
            if (!requstHeaders.ContainsKey(_requestHeaderToMatch))
            {
                return false;
            }
            var parsedRequestMediaType = new MediaType(requstHeaders[_requestHeaderToMatch]);

            foreach(var mediatype in _mediaTypes)
            {
                var parsedMediaType = new MediaType(mediatype);
                if(parsedRequestMediaType.Equals(parsedMediaType))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
