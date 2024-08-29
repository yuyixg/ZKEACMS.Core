/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy;
using System;
using System.Collections.Generic;
using ZKEACMS.Sitemap.Models;
using ZKEACMS.Sitemap.Service;

namespace ZKEACMS.Article.Service
{
    public class ArticlePageSiteUrlProvider : ISiteUrlProvider
    {
        private readonly IArticleUrlService _articleUrlService;

        public ArticlePageSiteUrlProvider(IArticleUrlService articleUrlService)
        {
            _articleUrlService = articleUrlService;
        }

        public IEnumerable<SiteUrl> Get()
        {
            foreach (var item in _articleUrlService.GetAllPublicUrls())
            {
                yield return new SiteUrl
                {
                    Url = Helper.Url.ToAbsolutePath(item.Url),
                    ModifyDate = item.Article.LastUpdateDate ?? DateTime.Now,
                    Changefreq = "daily",
                    Priority = 0.5F
                };
            }
        }
    }
}
