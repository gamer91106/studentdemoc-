using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tutorial_boostrap.Models;
using System.Data.Entity;
using Models;

namespace tutorial_boostrap.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
      }

        [HttpPost]
        public ActionResult LogIn(Models.UserModel user)

        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(Models.UserModel user)
        {
            return View();
       
        }

        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();

            bool IsValid = false;
            using (var db = new MandbEntities1())
            {
                var user = db.SystemUsers.FirstOrDefault(u => u.Email == email);

                if (user != null)
                { 
                if(user.Password == crypto.Compute(password, user.PasswordSalt)
                {
                IsValid = true;
                }
                }
            }
            return IsValid;
        }
  }
    
}
