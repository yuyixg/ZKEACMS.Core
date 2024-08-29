﻿/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy.Serializer;
using System;
using System.Net.Http;

namespace Easy.Net.WebApi
{
    public class JsonSerializer : IRequestSerializer
    {
        public string GetContentTypeRegexPattern()
        {
            return MimeContentType.Json;
        }

        public object DeserializeResponse(HttpContent content, Type responseType)
        {
            var jsonString = content.ReadAsStringAsync().Result;

            return JsonConverter.Deserialize(jsonString, responseType);
        }

        public HttpContent SerializeRequest(HttpRequest request)
        {
            return new StringContent(JsonConverter.Serialize(request.Body), System.Text.Encoding.UTF8, MimeContentType.Json);
        }
    }
}
