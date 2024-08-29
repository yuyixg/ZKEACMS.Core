/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy.Extend;
using Easy.Mvc.Authorize;
using Easy.Mvc.Resource;
using Easy.Mvc.Resource.Enums;
using Easy.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Easy.Mvc.RazorPages
{
    public abstract class EasyRazorPage<TModel> : RazorPage<TModel>
    {
        private List<ResourceCollection> _requiredScripts
        {
            get
            {
                const string key = "Requied_Scripts";
                if (ViewContext.HttpContext.Items.ContainsKey(key))
                {
                    return ViewContext.HttpContext.Items[key] as List<ResourceCollection>;
                }
                var scripts = new List<ResourceCollection>();
                ViewContext.HttpContext.Items.Add(key, scripts);
                return scripts;
            }
        }
        private List<ResourceCollection> _requiredStyles
        {
            get
            {
                const string key = "Requied_Styles";
                if (ViewContext.HttpContext.Items.ContainsKey(key))
                {
                    return ViewContext.HttpContext.Items[key] as List<ResourceCollection>;
                }
                var styles = new List<ResourceCollection>();
                ViewContext.HttpContext.Items.Add(key, styles);
                return styles;
            }
        }
        public IHtmlContent ScriptAtHead(bool includeRequired = true)
        {
            return GetResource(includeRequired, ResourceType.Script, ResourcePosition.Head);
        }

        public IHtmlContent ScriptAtFoot(bool includeRequired = true)
        {
            return GetResource(includeRequired, ResourceType.Script, ResourcePosition.Foot);
        }

        public IHtmlContent StyleAtHead(bool includeRequired = true)
        {
            return GetResource(includeRequired, ResourceType.Style, ResourcePosition.Head);
        }

        public IHtmlContent StyleAtFoot(bool includeRequired = true)
        {
            return GetResource(includeRequired, ResourceType.Style, ResourcePosition.Foot);
        }

        public LocalizeString L(string content)
        {
            return new LocalizeString(content, this.ViewContext.HttpContext);
        }

        public LocalizeString L(string content, string cultureCode)
        {
            return new LocalizeString(content, cultureCode, this.ViewContext.HttpContext);
        }

        private IHtmlContent GetResource(bool includeRequired, ResourceType type, ResourcePosition position)
        {
            var builder = new HtmlContentBuilder();
            IUrlHelper urlHelper = Context.RequestServices.GetService<IUrlHelperFactory>().GetUrlHelper(ViewContext);
            IWebHostEnvironment hostingEnvironment = Context.RequestServices.GetService<IWebHostEnvironment>();
            IOptions<CDNOption> options = Context.RequestServices.GetService<IOptions<CDNOption>>();
            switch (type)
            {
                case ResourceType.Script:
                    {
                        if (includeRequired)
                        {
                            ResourceHelper.ScriptSource.Where(m => m.Value.Required && m.Value.Position == position)
                                                    .Each(m => m.Value.Each(r =>
                                                    {
                                                        builder.AppendHtml(r.ToSource(urlHelper, hostingEnvironment, options));
                                                    }));
                        }

                        _requiredScripts.Where(m => m.Position == position).Each(m => m.Each(r =>
                        builder.AppendHtml(r.ToSource(urlHelper, hostingEnvironment, options))));
                        break;
                    }

                case ResourceType.Style:
                    {
                        if (includeRequired)
                        {
                            ResourceHelper.StyleSource.Where(m => m.Value.Required && m.Value.Position == position)
                                                        .Each(m => m.Value.Each(r =>
                                                        {
                                                            builder.AppendHtml(r.ToSource(urlHelper, hostingEnvironment, options));
                                                        }));
                        }

                        _requiredStyles.Where(m => m.Position == position).Each(m => m.Each(r =>
                        {
                            builder.AppendHtml(r.ToSource(urlHelper, hostingEnvironment, options));
                        }));
                        break;
                    }
            }
            return builder;
        }

        public ScriptRegister Script
        {
            get
            {
                return new ScriptRegister(this, RegistScript);
            }
        }

        public StyleRegister Style
        {
            get
            {
                return new StyleRegister(this, RegistStyle);
            }
        }
        private void RegistStyle(ResourceCollection resource)
        {
            if (_requiredStyles.All(m => m.Name != resource.Name))
            {
                _requiredStyles.Add(resource);
            }
        }
        private void RegistScript(ResourceCollection resource)
        {
            if (_requiredScripts.All(m => m.Name != resource.Name))
            {
                _requiredScripts.Add(resource);
            }
        }
        public IApplicationContext ApplicationContext
        {
            get
            {
                return ViewContext.HttpContext.RequestServices.GetService<IApplicationContext>();
            }
        }
        public IAuthorizer Authorizer
        {
            get
            {
                return ViewContext.HttpContext.RequestServices.GetService<IAuthorizer>();
            }
        }
    }
}