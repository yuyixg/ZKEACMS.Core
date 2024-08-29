/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy;
using Easy.Constant;
using Easy.Extend;
using System.Text.RegularExpressions;
using ZKEACMS.FormGenerator.Models;

namespace ZKEACMS.FormGenerator.Service.Validator
{
    public class EmailFormDataValidator : IFormDataValidator
    {
        private readonly ILocalize _localize;
        public EmailFormDataValidator(ILocalize localize)
        {
            _localize = localize;
        }
        public bool Validate(FormField field, FormDataItem data, out string message)
        {
            message = string.Empty;
            if (field.Name == "Email" && data.FieldValue.IsNotNullAndWhiteSpace() && !Regex.IsMatch(data.FieldValue, RegularExpression.Email))
            {
                message = _localize.Get("Invalid Email for {0}.").FormatWith(field.DisplayName);
                return false;
            }
            return true;
        }
    }
}
