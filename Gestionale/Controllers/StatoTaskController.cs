using Gestionale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestionale.Controllers
{
    public class StatoTaskController : Controller
    {
        // GET: StatoTask
        public ActionResult Index()
        {
            return View();
        }

        // GET: StatoTask/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StatoTask/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatoTask/Create
        [HttpPost]
        public ActionResult Create(StatoTask stato)
        {
            try
            {
                // TODO: Add insert logic here

                
            }
            catch
            {
            }
                return View();
        }

        // GET: StatoTask/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StatoTask/Edit/5
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

        // GET: StatoTask/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StatoTask/Delete/5
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
