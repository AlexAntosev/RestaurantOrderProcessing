using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RestaurantOrderProcessing.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.WebUI.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
        
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new UserRole { Name = model.Name, Description = model.Description });
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cannot create new role");
                }
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(string Id)
        {
            UserRole role = await RoleManager.FindByIdAsync(Id);
            if(role != null)
            {
                return View(new EditRoleViewModel { Id = role.Id, Name = role.Name, Description = role.Description });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                UserRole role = await RoleManager.FindByIdAsync(model.Id);
                if(role != null)
                {
                    role.Name = model.Name;
                    role.Description = model.Description;
                    IdentityResult result = await RoleManager.UpdateAsync(role);
                    if(result.Succeeded)
                    {

                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Cannot edit role");
                    }
                }                
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(string Id)
        {
            UserRole role = await RoleManager.FindByIdAsync(Id);
            if(role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
    }
}