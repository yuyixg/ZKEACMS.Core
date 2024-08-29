/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy;
using ZKEACMS.FormGenerator.Models;
using ZKEACMS.Widget;

namespace ZKEACMS.FormGenerator.Service
{
    public class FormWidgetService : SimpleWidgetService<FormWidget>
    {
        private readonly IFormService _formService;
        public FormWidgetService(IWidgetBasePartService widgetBasePartService,
            IApplicationContext applicationContext,
            CMSDbContext dbContext,
            IFormService formService)
            : base(widgetBasePartService, applicationContext, dbContext)
        {
            _formService = formService;
        }

        public override object Display(WidgetDisplayContext widgetDisplayContext)
        {
            if (widgetDisplayContext.FormModel is Form form)
            {
                return form;
            }
            form = _formService.Get((widgetDisplayContext.Widget as FormWidget).FormID);
            return form;
        }
    }
}
