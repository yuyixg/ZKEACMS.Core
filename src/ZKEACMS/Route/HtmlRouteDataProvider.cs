/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Microsoft.AspNetCore.Routing;
using System;

namespace ZKEACMS.Route
{
    public class HtmlRouteDataProvider : IRouteDataProvider
    {
        const string htmlExt = ".html";

        public int Order { get { return 0; } }

        public string ExtractVirtualPath(string path, RouteValueDictionary values)
        {
            if (path.EndsWith(htmlExt, StringComparison.OrdinalIgnoreCase))
            {
                path = path.Substring(0, path.LastIndexOf(htmlExt, StringComparison.OrdinalIgnoreCase));
            }
            return path;
        }
    }
}
