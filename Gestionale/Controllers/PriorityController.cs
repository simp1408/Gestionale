using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestionale.Controllers
{
    public class PriorityController : Controller
    {
        // GET: Priority
        public ActionResult Index()
        {
            return View();
        }

        // GET: Priority/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Priority/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Priority/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Priority/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Priority/Edit/5
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

        // GET: Priority/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Priority/Delete/5
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
