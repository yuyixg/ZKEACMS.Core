/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy.RepositoryPattern;
using ZKEACMS.Common.Models;

namespace ZKEACMS.Common.Service
{
    public interface INavigationService : IService<NavigationEntity>
    {
        void Move(string id, string parentId, int position, int oldPosition);
    }
}