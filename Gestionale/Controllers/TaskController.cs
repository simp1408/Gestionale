using Antlr.Runtime.Misc;
using Gestionale.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Gestionale.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            return View(Task.GetTask());
            
        }


       
        public ActionResult ListaTask()
        {
            string username = User.Identity.Name;
            int id = Task.GetUserId(username);
            ViewBag.listaStatoTask = StatoTask.StatoTaskSelect();

            if (id != 0)
            {
                return View (Task.GetUserTasks(id));
             

            }

            return View();


        }


        [HttpPost]
        public JsonResult ListaTaskJson(int Drop)
        {
            string username = User.Identity.Name;
            int id = Task.GetUserId(username);
            ViewBag.listaStatoTask = StatoTask.StatoTaskSelect();
            Task t= Task.GetOneTask(id);
            StatoTask Stato = StatoTask.GetStato(Drop);
            if (id != 0)
            {
                //Task task= Task.EditStato(t, Stato);
                return Json(Task.EditStato(t, Stato), JsonRequestBehavior.AllowGet);


            }

            return Json(null, JsonRequestBehavior.AllowGet);


        }



        public ActionResult Details()
        {
            return View();
        }

        // GET: Task/Create
        public ActionResult CreateTask()
        {
            ViewBag.listaUtente = Utente.UtenteSelect();
            ViewBag.listaStatoTask=StatoTask.StatoTaskSelect();
            ViewBag.listaPriority = Priority.PrioritySelect();
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult CreateTask(Task t)
        {
            SqlConnection sql=Shared.GetConnection();
            try
            {
                sql.Open();
                SqlCommand command = Shared.GetCommand("INSERT INTO TASK values(@DescrizioneTask,@DataInizio,@DataScadenza,@IdUtente,@IdStatoTask,@IdPriority)", sql);
                command.Parameters.AddWithValue("@DescrizioneTask", t.DescrizioneTask);
                command.Parameters.AddWithValue("@DataInizio", t.DataInizio);
                command.Parameters.AddWithValue("@DataScadenza", t.DataScadenza);
                command.Parameters.AddWithValue("@IdUtente", t.IdUtente);
                command.Parameters.AddWithValue("@IdStatoTask", t.IdStatoTask);
                command.Parameters.AddWithValue("@IdPriority", t.IdPriority);
                int row= command.ExecuteNonQuery();
                if(row > 0)
                {
                    ViewBag.listaUtente = Utente.UtenteSelect();
                    ViewBag.listaStatoTask = StatoTask.StatoTaskSelect();
                    ViewBag.listaPriority = Priority.PrioritySelect();
                    ViewBag.ConfirmMessage = "Task aggiunta con successo";
                }

                
            }
            catch(Exception ex)
            {
                return null;
            }
            finally { 
             sql.Close();
            }
                return View();
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
           
            return View();




        }

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit(Task t)
        {
            return View();
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Task/Delete/5
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

        public ActionResult PartialViewTask()
        {
            List<Task> listTask = new List<Task>();
            listTask = Task.GetTask();
            return PartialView("_PartialViewTask", listTask);
        }


        //public ActionResult TaskOne()
        //{
          
            
        //}




    }
}
