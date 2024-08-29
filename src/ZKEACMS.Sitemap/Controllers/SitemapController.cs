/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Microsoft.AspNetCore.Mvc;
using ZKEACMS.Sitemap.Service;

namespace ZKEACMS.Sitemap.Controllers
{
    public class SitemapController : Controller
    {
        private readonly ISitemapService _sitemapService;
        public SitemapController(ISitemapService sitemapService)
        {
            _sitemapService = sitemapService;
        }
        public IActionResult Index()
        {
            return Content(_sitemapService.Get(), "application/xml");
        }
    }
}
