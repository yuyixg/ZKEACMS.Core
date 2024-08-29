/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy.RepositoryPattern;
using Microsoft.AspNetCore.Http;
using System.IO;
using ZKEACMS.FormGenerator.Models;

namespace ZKEACMS.FormGenerator.Service
{
    public interface IFormDataService : IService<FormData>
    {
        ServiceResult<FormData> SaveForm(IFormCollection form, string formId);
        MemoryStream Export(int id);
        MemoryStream ExportByForm(string formId);
    }
}
