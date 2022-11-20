using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Practica03_G1.Models;

namespace MvcFormAuthentication_Demo.Controllers
{  
	      
    public class HomeController : Controller  
    {  
        private readonly Practica04Entities _dbContext = new Practica04Entities();  
        public ActionResult Index()
        {  
            return View();  
        }  
        public ActionResult Login()
        {  
            return View();  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user) //ojo
        {
             if (ModelState.IsValid)
                    {
                       bool IsValidUser = _dbContext.Users
                       .Any(u => u.Username.ToLower() == user
                       .Username.ToLower() && user
                       .Password == user.Password);
        
                       if (IsValidUser)
                      {
            FormsAuthentication.SetAuthCookie(user.Username, false);
                                 return RedirectToAction("Index", "Employee");
                             }
                     }
    ModelState.AddModelError("", "invalid Username or Password");
             return View();
             }
         public ActionResult Register()
         {
                 return View();
             }
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Register(Users registerUser) //ojo
        {
                 if (ModelState.IsValid)
                     {
        _dbContext.Users.Add(registerUser);
        _dbContext.SaveChanges();
                         return RedirectToAction("Login");
        
                     }
                 return View();
             }
         public ActionResult Logout()
         {
    FormsAuthentication.SignOut();
                 return RedirectToAction("Login");
             }
     }  
}  
