using Gestionale.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Gestionale.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //[ActionName("Login")]
        public ActionResult Login(Utente u)
        {
            SqlConnection sql = Shared.GetConnection();

            sql.Open();
            SqlCommand command = Shared.GetCommand("select * from Utente where Username= @Username and Password=@Password", sql);
            command.Parameters.AddWithValue("@Username", u.Username);
            command.Parameters.AddWithValue("@Password", u.Password);
           

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                   FormsAuthentication.SetAuthCookie(u.Username, false);
                // prendo l id del utente
             


                if (u.Username =="admin")
                {
                    return RedirectToAction("Dipendenti","Utente");
                }
                else
                {
                    //string username = User.Identity.Name;
                    //int id= Task.GetUserId(username);
                    return RedirectToAction("ListaTask","Task");
                }


            }
            else
            {
                ViewBag.ErrorAutentication = "Username e/o Password errate";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl); 
        }

        public ActionResult Note()
        {
          


            return View();
        }

    
    }
}