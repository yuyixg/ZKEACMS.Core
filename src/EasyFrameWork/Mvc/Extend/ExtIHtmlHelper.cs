/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy.ViewPort.jsTree;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Easy.Mvc.Extend
{
    public static class ExtIHtmlHelper
    {
        public static Tree<T> Tree<T>(this IHtmlHelper htmlHelper) where T : class
        {
            return new Tree<T>(htmlHelper.ViewContext);
        }
    }
}
