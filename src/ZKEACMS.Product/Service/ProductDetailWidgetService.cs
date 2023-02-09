/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using System;
using Easy;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ZKEACMS.Product.Models;
using ZKEACMS.Widget;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Easy.Extend;
using System.Collections.Concurrent;
using Easy.Constant;

namespace ZKEACMS.Product.Service
{
    public class ProductDetailWidgetService : WidgetService<ProductDetailWidget>
    {
        private const string ProductDetailWidgetRelatedPageUrls = "ProductDetailWidgetRelatedPageUrls";
        private readonly ConcurrentDictionary<string, object> _allRelatedUrlCache;
        private readonly IProductService _productService;
        public ProductDetailWidgetService(IWidgetBasePartService widgetService,
            IProductService productService,
            IApplicationContext applicationContext,
            Easy.Cache.ICacheManager<ConcurrentDictionary<string, object>> cacheManager,
            CMSDbContext dbContext)
            : base(widgetService, applicationContext, dbContext)
        {
            _productService = productService;
            _allRelatedUrlCache = cacheManager.GetOrAdd(ProductDetailWidgetRelatedPageUrls, new ConcurrentDictionary<string, object>());
        }
        private void DismissRelatedPageUrls()
        {
            _allRelatedUrlCache.TryRemove(ProductDetailWidgetRelatedPageUrls, out var urls);
        }

        public override void AddWidget(WidgetBase widget)
        {
            base.AddWidget(widget);
            DismissRelatedPageUrls();
        }

        public override void DeleteWidget(string widgetId)
        {
            base.DeleteWidget(widgetId);
            DismissRelatedPageUrls();
        }
        public override object Display(WidgetDisplayContext widgetDisplayContext)
        {
            var actionContext = widgetDisplayContext.ActionContext;
            int productId = actionContext.RouteData.GetPost();
            ProductEntity product = null;
            if (productId != 0)
            {
                product = actionContext.RouteData.GetProduct(productId) ?? _productService.Get(productId);
                if (!product.IsPublish || product.Status != (int)RecordStatus.Active)
                {
                    actionContext.NotFoundResult();
                    return null;
                }
                if (product != null && product.Url.IsNotNullAndWhiteSpace() && actionContext.RouteData.GetProductUrl().IsNullOrWhiteSpace())
                {
                    actionContext.RedirectTo($"{actionContext.RouteData.GetPath()}/{product.Url}.html", true);
                }
            }
            if (product == null && ApplicationContext.IsAuthenticated)
            {
                product = _productService.Get().OrderByDescending(m => m.ID).FirstOrDefault();
                if (product != null)
                {
                    product = _productService.Get(product.ID);
                }
            }
            if (product == null)
            {
                actionContext.NotFoundResult();
            }
            if (product != null)
            {
                var layout = widgetDisplayContext.PageLayout;
                if (layout != null && layout.Page != null)
                {
                    layout.Page.ConfigSEO(product.SEOTitle ?? product.Title, product.SEOKeyWord, product.SEODescription);
                }
            }

            return product ?? new ProductEntity();
        }

        public string[] GetRelatedPageUrls()
        {
            return _allRelatedUrlCache.GetOrAdd(ProductDetailWidgetRelatedPageUrls, fac =>
            {
                var pages = WidgetBasePartService.Get(w => Get().Select(m => m.ID).Contains(w.ID)).Select(m => m.PageId).ToArray();
                return DbContext.Page.Where(p => pages.Contains(p.ID)).Select(m => m.Url.Replace("~/", "/")).Distinct().ToArray();
            }) as string[];
        }
    }
}