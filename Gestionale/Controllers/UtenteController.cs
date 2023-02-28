using Gestionale.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Gestionale.Controllers
{
    public class UtenteController : Controller
    {
        // GET: Utente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dipendenti()
        {
            return View(Utente.GetAllUtente());
        }

        // GET: Utente/Details/5
        public ActionResult Details(int id)
        {
            SqlConnection sql = Shared.GetConnection();
            try
            {
                sql.Open();
                SqlCommand command = Shared.GetCommand("SELECT * FROM Utente inner join mansioni on Utente.IdMansioni=Mansioni.IDmansioni where IDutente=@IDutente", sql);
                command.Parameters.AddWithValue("@IDutente", id);
                SqlDataReader reader = command.ExecuteReader();

                Utente u = new Utente();
                     
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //u.IDutente = Convert.ToInt32(reader["IDutente"]);
                        Mansioni mansioni= new Mansioni();
                        mansioni.Descrizione = reader["Descrizione"].ToString();
                        u.IDutente = id;
                        u.Nome = reader["Nome"].ToString();
                        u.Cognome = reader["Cognome"].ToString();
                        u.DataNascita = Convert.ToDateTime(reader["DataNascita"]);
                        u.Cod_fisc = reader["Cod_fisc"].ToString();
                        u.Indirizzo = reader["Indirizzo"].ToString();
                        u.Citta = reader["Citta"].ToString();
                        u.Provincia = reader["Provincia"].ToString();
                        u.Email = reader["Email"].ToString();
                        u.Telefono = reader["Telefono"].ToString();
                        u.Ruolo = reader["Ruolo"].ToString();
                        u.Username = reader["username"].ToString();
                        u.Password = reader["password"].ToString();
                        u.Foto = reader["Foto"].ToString();
                        u.DataAssunzione = Convert.ToDateTime(reader["DataAssunzione"]);
                        u.Stipendio = Convert.ToDecimal(reader["Stipendio"]);
                        u.IdMansioni = Convert.ToInt32(reader["IdMansioni"]);
                        u.Mansioni = mansioni;
                      

                    }
                }
              
                return View(u);

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                sql.Close();
            }
        }
    

        // GET: Utente/Create
        public ActionResult Create()
        {
            ViewBag.listaMansioni = Mansioni.MansioniSelect();
            return View();
        }

        // POST: Utente/Create
        [HttpPost]
        public ActionResult Create(Utente u, HttpPostedFileBase FileUpload)
        {
            SqlConnection sql = Shared.GetConnection();
            try
            {
               sql.Open();

                string fileName = "";
                if (FileUpload != null)
                {
                    if (FileUpload.ContentLength > 0)
                    {
                        fileName = FileUpload.FileName;
                        //andiamo a salvare il file immagine mettendoci il percorso
                        string Path = Server.MapPath("/Content/img/" + fileName);
                        FileUpload.SaveAs(Path);

                    }
                }


                SqlCommand command = Shared.GetCommand("insert into Utente values" + 
                    "(@Nome,@Cognome,@DataNascita,@Cod_fisc,@Indirizzo,@Citta,@Provincia,@Email,@Telefono,@Ruolo,@Username,@Password,@Foto,@DataAssunzione,@Stipendio,@IdMansioni)", sql);
                command.Parameters.AddWithValue("Nome", u.Nome);
                command.Parameters.AddWithValue("Cognome", u.Cognome);
                command.Parameters.AddWithValue("DataNascita", u.DataNascita);
                command.Parameters.AddWithValue("Cod_fisc", u.Cod_fisc);
                command.Parameters.AddWithValue("Indirizzo", u.Indirizzo);
                command.Parameters.AddWithValue("Citta", u.Citta);
                command.Parameters.AddWithValue("Provincia", u.Provincia);
                command.Parameters.AddWithValue("Email", u.Email);
                command.Parameters.AddWithValue("Telefono", u.Telefono);
                command.Parameters.AddWithValue("Ruolo", u.Ruolo);
                command.Parameters.AddWithValue("Username", u.Username);
                command.Parameters.AddWithValue("Password", u.Password);
                command.Parameters.AddWithValue("Foto", fileName);
                command.Parameters.AddWithValue("DataAssunzione", u.DataAssunzione);
                command.Parameters.AddWithValue("Stipendio", u.Stipendio);
                command.Parameters.AddWithValue("IdMansioni", u.IdMansioni);
                int row= command.ExecuteNonQuery();
                if (row > 0)
                {
                    ViewBag.listaMansioni = Mansioni.MansioniSelect();
                    ViewBag.ConfirmMessage="Dipendente aggiunto con successo";
                }

            }
            catch
            {
                return View();
            }
            finally { sql.Close(); }


            return View();

        }

        // GET: Utente/Edit/5
        public ActionResult Edit(int id)
        {
            SqlConnection sql = Shared.GetConnection();


            try
            {
                sql.Open();
                SqlCommand command = Shared.GetCommand("SELECT * FROM Utente inner join mansioni on Utente.IdMansioni=Mansioni.IDmansioni where IDutente=@IDutente", sql);

                command.Parameters.AddWithValue("@IDutente", id);

                SqlDataReader reader = command.ExecuteReader();
                Utente u = new Utente();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Mansioni mansioni = new Mansioni();
                        mansioni.IDmansioni = Convert.ToInt32(reader["IDmansioni"]);
                        mansioni.Descrizione = reader["Descrizione"].ToString();
                        u.IDutente = id;
                        u.Nome = reader["Nome"].ToString();
                        u.Cognome = reader["Cognome"].ToString();
                        u.DataNascita = Convert.ToDateTime(reader["DataNascita"]);
                        u.Cod_fisc = reader["Cod_fisc"].ToString();
                        u.Indirizzo = reader["Indirizzo"].ToString();
                        u.Citta = reader["Citta"].ToString();
                        u.Provincia = reader["Provincia"].ToString();
                        u.Email = reader["Email"].ToString();
                        u.Telefono = reader["Telefono"].ToString();
                        u.Ruolo = reader["Ruolo"].ToString();
                        u.Username = reader["username"].ToString();
                        u.Password = reader["password"].ToString();
                        u.Foto = reader["Foto"].ToString();
                        u.DataAssunzione = Convert.ToDateTime(reader["DataAssunzione"]);
                        u.Stipendio = Convert.ToDecimal(reader["Stipendio"]);
                        u.IdMansioni = Convert.ToInt32(reader["IdMansioni"]);
                        u.Mansioni = mansioni;

                    }
                }
                if (u.Foto == null)
                {
                    TempData["UrlFoto"] = "no Foto";

                }
                else if (u.Foto == "")
                {
                    TempData["UrlFoto"] = "no Foto";

                }
                else
                {
                    TempData["UrlFoto"] = u.Foto;
                }

                ViewBag.listaMansioni = Mansioni.MansioniSelect();
                return View(u);

            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {

                sql.Close();
            }
        }

        // POST: Utente/Edit/5
        [HttpPost]
        public ActionResult Edit(Utente u, HttpPostedFileBase FileUpload)
        {
            SqlConnection sql = Shared.GetConnection();
            try
            
            {
                sql.Open();

                string fileName = "";
                if (u.Foto== null)
                {

                    u.Foto = TempData["UrlFoto"].ToString();
                }
                  else
                   {
                        fileName = FileUpload.FileName;
                         u.Foto = fileName;
                        string Path = Server.MapPath("/Content/img/" + fileName);
                        //andiamo a salvare il file immaggine mettendoci il percorso
                        FileUpload.SaveAs(Path);

                    }


                SqlCommand command = Shared.GetCommand("Update Utente set  Nome=@Nome, Cognome=@Cognome,DataNascita=@DataNascita,Cod_fisc=@Cod_fisc,Indirizzo=@Indirizzo,Citta=@Citta,Provincia=@Provincia,Email=@Email,Telefono=@Telefono,Ruolo=@Ruolo,Username=@Username,Password=@Password,Foto=@Foto,DataAssunzione=@DataAssunzione,Stipendio=@Stipendio,IdMansioni=@IdMansioni where IDutente=@IDutente",sql);
                command.Parameters.AddWithValue("@IDutente", u.IDutente);
                command.Parameters.AddWithValue("@Nome", u.Nome);
                command.Parameters.AddWithValue("@Cognome", u.Cognome);
                command.Parameters.AddWithValue("@DataNascita", u.DataNascita);
                command.Parameters.AddWithValue("@Cod_fisc", u.Cod_fisc);
                command.Parameters.AddWithValue("@Indirizzo", u.Indirizzo);
                command.Parameters.AddWithValue("@Citta", u.Citta);
                command.Parameters.AddWithValue("@Provincia", u.Provincia);
                command.Parameters.AddWithValue("@Email", u.Email);
                command.Parameters.AddWithValue("@Telefono", u.Telefono);
                command.Parameters.AddWithValue("@Ruolo", u.Ruolo);
                command.Parameters.AddWithValue("@Username", u.Username);
                command.Parameters.AddWithValue("@Password", u.Password);
                command.Parameters.AddWithValue("@Foto", u.Foto);
                command.Parameters.AddWithValue("@DataAssunzione", u.DataAssunzione);
                command.Parameters.AddWithValue("@Stipendio", u.Stipendio);
                command.Parameters.AddWithValue("@IdMansioni", u.IdMansioni);
              


                int row = command.ExecuteNonQuery();
               

            }
            catch(Exception ex)
            {
                
            }
            finally { sql.Close(); }

            return RedirectToAction("Dipendenti");

        }

        // GET: Utente/Delete/5
        public ActionResult Delete(int id)
        {
            SqlConnection sql= Shared.GetConnection();
            try { 

                sql.Open();
                SqlCommand command = Shared.GetCommand("Select * from Utente where IDutente=@IDutente", sql);
                command.Parameters.AddWithValue("@IDutente", id);

                SqlDataReader reader= command.ExecuteReader();
                   Utente u = new Utente();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        u.IDutente = id;
                        u.Nome = reader["Nome"].ToString();
                        u.Cognome = reader["Cognome"].ToString();
                        u.DataNascita = Convert.ToDateTime(reader["DataNascita"]);
                        u.Cod_fisc = reader["Cod_fisc"].ToString();
                        u.Indirizzo = reader["Indirizzo"].ToString();
                        u.Citta = reader["Citta"].ToString();
                        u.Provincia = reader["Provincia"].ToString();
                        u.Email = reader["email"].ToString();
                        u.Telefono = reader["Telefono"].ToString();
                        u.Ruolo = reader["Ruolo"].ToString();
                        u.Username = reader["username"].ToString();
                        u.Password = reader["password"].ToString();
                        u.Foto = reader["Foto"].ToString();
                        u.DataAssunzione = Convert.ToDateTime(reader["DataAssunzione"]);
                        u.Stipendio = Convert.ToDecimal(reader["Stipendio"]);
                        u.IdMansioni = Convert.ToInt32(reader["IdMansioni"]);

                    }
                }
              
            return View(u);
            }
            catch(Exception ex) 
            { return null; } 
            finally { 
                sql.Close(); 
            }

        }

        // POST: Utente/Delete/5
        [HttpPost]
        [ActionName("Delete")]

        public ActionResult DeleteDipendente(int id)
        {
            SqlConnection sql = Shared.GetConnection();
            try
            {

                sql.Open();
                SqlCommand command = Shared.GetCommand("Delete from Utente where IDutente=@IDutente", sql);
                command.Parameters.AddWithValue("@IDutente", id);

                SqlDataReader reader = command.ExecuteReader();
                Utente u = new Utente();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        u.IDutente = id;
                        u.Nome = reader["Nome"].ToString();
                        u.Cognome = reader["Cognome"].ToString();
                        u.DataNascita = Convert.ToDateTime(reader["DataNascita"]);
                        u.Cod_fisc = reader["Cod_fisc"].ToString();
                        u.Indirizzo = reader["Indirizzo"].ToString();
                        u.Citta = reader["Citta"].ToString();
                        u.Provincia = reader["Provincia"].ToString();
                        u.Email = reader["email"].ToString();
                        u.Telefono = reader["Telefono"].ToString();
                        u.Ruolo = reader["Ruolo"].ToString();
                        u.Username = reader["username"].ToString();
                        u.Password = reader["password"].ToString();
                        u.Foto = reader["Foto"].ToString();
                        u.DataAssunzione = Convert.ToDateTime(reader["DataAssunzione"]);
                        u.Stipendio = Convert.ToDecimal(reader["Stipendio"]);
                        u.IdMansioni = Convert.ToInt32(reader["IdMansioni"]);

                    }
                }

                return View(u);
            }
            catch (Exception ex)
            { return null; }
            finally
            {
                sql.Close();
            }
        }

        //partial view
        public ActionResult PartialViewDipendente()
        {
            List<Utente> listDipendenti = new List<Utente>();
            listDipendenti = Utente.GetAllUtente();
            return PartialView("_PartialViewDipendente",listDipendenti);
        }


        //qui ho il form per la ricerca del dipendente
        public ActionResult SearchForm()
        {
            
            return View();
        }

        //Search Name mi restituisce la lista con il dipendente cercato
        public ActionResult SearchName(string searchPhrase)
        {
            //List<Utente> SearchResult = new List<Utente>();
            //SearchResult.Add(Utente.SearchName(searchPhrase).ToList());
            
            return View(Utente.SearchName(searchPhrase));


        }

        

    }
    }

