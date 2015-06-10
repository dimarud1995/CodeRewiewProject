using SqlGIS.Infrastructure.Abstract;
using SqlGIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SqlGIS.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        IAuthProvider authProvider;
        public AccountController(IAuthProvider auth) 
        {
            authProvider = auth;
        }

        public ViewResult Login() 
        {
            return View();
        }

        [HttpPost]
        //log in for editing data(tables) 
        public ActionResult Login(LoginViewModel model, string returnUrl) 
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("ClientsList", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else 
            {
                return View();
            }
        }
    }
}
