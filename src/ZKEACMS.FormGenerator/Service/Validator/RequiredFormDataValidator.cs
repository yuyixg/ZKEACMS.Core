/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy;
using Easy.Extend;
using ZKEACMS.FormGenerator.Models;

namespace ZKEACMS.FormGenerator.Service.Validator
{
    public class RequiredFormDataValidator : IFormDataValidator
    {
        private readonly ILocalize _localize;
        public RequiredFormDataValidator(ILocalize localize)
        {
            _localize = localize;
        }
        public bool Validate(FormField field, FormDataItem data, out string message)
        {
            message = string.Empty;
            if (field.IsRequired && data.FieldValue.IsNullOrWhiteSpace() && data.OptionValue.IsNullOrWhiteSpace())
            {
                message = field.RequiredMessage ?? _localize.Get("{0} is required").FormatWith(field.DisplayName);
                return false;
            }
            return true;
        }
    }
}
