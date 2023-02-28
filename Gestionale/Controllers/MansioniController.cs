using Gestionale.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestionale.Controllers
{
    public class MansioniController : Controller
    {
        // GET: Mansioni
        public ActionResult Index()
        {
            return View();
        }

        // GET: Mansioni/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mansioni/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mansioni/Create
        [HttpPost]
        public ActionResult Create( Mansioni m)
        {
            SqlConnection sql= Shared.GetConnection();
            try
            {
                sql.Open();
                SqlCommand command = Shared.GetCommand("insert into mansioni values(@Descrizione)",sql);
                command.Parameters.AddWithValue("@Descrizione", m.Descrizione);
                int row= command.ExecuteNonQuery();
                if(row > 0)
                {
                    ViewBag.SuccessMessage = "Mansione aggiunga con successo";
                    return View();
                }
               
            }
            catch(Exception ex)
            {
                return null;
            }
            finally { sql.Close(); }    
              return View();
        }

        // GET: Mansioni/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mansioni/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mansioni/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mansioni/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
