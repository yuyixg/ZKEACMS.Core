/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy.Constant;
using Easy.Extend;
using Easy.Modules.Role;
using Easy.Mvc.Authorize;
using Easy.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ZKEACMS.Controllers
{
    [DefaultAuthorize(Policy = PermissionKeys.ViewRole)]
    public class RolesController : BasicController<RoleEntity, int, IRoleService>
    {
        public RolesController(IRoleService userService)
            : base(userService)
        {
        }
        [NonAction]
        public override IActionResult Create(RoleEntity entity)
        {
            return base.Create(entity);
        }
        [HttpPost, DefaultAuthorize(Policy = PermissionKeys.ManageRole)]
        public IActionResult Create(RoleEntity entity, List<Easy.Modules.Role.PermissionDescriptor> PermissionSet)
        {
            var result = Service.Add(entity, PermissionSet);
            if (result.HasViolation)
            {
                foreach (var item in result.RuleViolations)
                {
                    ModelState.AddModelError(item.ParameterName, item.ErrorMessage);
                }
                return View(entity);
            }
            if (entity.ActionType.HasFlag(ActionType.Exit))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { Id = entity.ID });
        }
        public override IActionResult Edit(int Id)
        {
            ViewBag.Permissions = Service.GetPermission(Id);
            return base.Edit(Id);
        }

        [NonAction]
        public override IActionResult Edit(RoleEntity entity)
        {
            return base.Edit(entity);
        }

        [HttpPost, DefaultAuthorize(Policy = PermissionKeys.ManageRole)]
        public IActionResult Edit(RoleEntity entity, List<Easy.Modules.Role.PermissionDescriptor> PermissionSet)
        {
            var result = Service.Update(entity, PermissionSet);
            if (result.HasViolation)
            {
                ViewBag.Permissions = Service.GetPermission(entity.ID);
                foreach (var item in result.RuleViolations)
                {
                    ModelState.AddModelError(item.ParameterName, item.ErrorMessage);
                }
                return View(entity);
            }
            if (entity.ActionType.HasFlag(ActionType.Exit))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { Id = entity.ID });
        }

        [HttpPost, DefaultAuthorize(Policy = PermissionKeys.ManageRole)]
        public override IActionResult Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
