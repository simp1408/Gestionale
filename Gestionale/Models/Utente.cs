using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestionale.Models
{
    public class Utente
    {
        public int IDutente { get; set; }

        [Display(Name = "Nome Dipendente")]
        [Required(ErrorMessage ="Il nome del Dipendente è obbligatorio")]
        public string Nome { get; set; }
        [Display(Name = "Cognome Dipendente")]
        [Required(ErrorMessage = "Il Cognome del Dipendente è obbligatorio")]

        public string  Cognome { get; set; }

        [Display(Name = "Data di nascita")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "La data è obbligatoria")]
        public DateTime  DataNascita { get; set; }

        [Display(Name = "Codice fiscale")]
        [Required(ErrorMessage = "Codice Fiscale è obbligatorio")]
        public string Cod_fisc { get; set; }
        [Display(Name = "Indirizzo")]
        [Required(ErrorMessage = "Indirizzo è obbligatorio")]

        public string Indirizzo { get; set; }
        [Required(ErrorMessage ="La città è obbligatoria")]
        [Display(Name = "Città")]
        public string Citta { get; set; }
        [Display(Name = "Prov")]
        [Required(ErrorMessage = "La provincia è obbligatorio")]

        public string Provincia { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email è obbligatorio")]
        public string Email { get; set; }


        [Required(ErrorMessage ="Il telefono è obbligatorio")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Il ruolo è obbligatorio")]
        public string Ruolo { get; set; }


        [Required(ErrorMessage = "Username è obbligatorio")]
        public string Username { get; set; }


        [Required(ErrorMessage = "La password è obbligatoria")]
        public string Password { get; set; }
       

        public string Foto { get; set; }
        [Required(ErrorMessage = "data assunzioe è obbligatoria")]


        [Display(Name = "Data Assunzione")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataAssunzione { get; set; }

        [Required(ErrorMessage = "Stipendio è obbligatorio")]
        public decimal Stipendio { get; set; }
        [Display(Name = "Qualifica")]
        public int IdMansioni { get; set; }
        public Mansioni Mansioni { get; set; }

        public static  List<Utente>GetAllUtente()
        {
            List<Utente>ListaUtenti= new List<Utente>();
            SqlConnection sql= Shared.GetConnection();
            try
            {
                sql.Open();
                SqlCommand command = Shared.GetCommand("select * from Utente inner join mansioni on utente.IdMansioni = Mansioni.IDmansioni",sql);
                
                SqlDataReader reader= command.ExecuteReader();


                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Utente u = new Utente();
                        Mansioni m = new Mansioni();
                        m.Descrizione = reader["Descrizione"].ToString();
                        u.IDutente = Convert.ToInt32(reader["IDutente"]);
                        u.Nome = reader["Nome"].ToString();
                        u.Cognome = reader["Cognome"].ToString();
                        u.DataNascita = Convert.ToDateTime(reader["DataNascita"]);
                        u.Cod_fisc = reader["Cod_fisc"].ToString();
                        u.Indirizzo = reader["Indirizzo"].ToString();
                        u.Citta = reader["Citta"].ToString() ;
                        u.Provincia = reader["Provincia"].ToString();
                        u.Email = reader["email"].ToString();
                        u.Telefono = reader["Telefono"].ToString();
                        u.Ruolo = reader["Ruolo"].ToString();
                        u.Username= reader["username"].ToString();
                        u.Password= reader["password"].ToString();
                        u.Foto = reader["Foto"].ToString();
                        u.DataAssunzione = Convert.ToDateTime(reader["DataAssunzione"]);
                        u.Stipendio = Convert.ToDecimal(reader["Stipendio"]);
                        u.IdMansioni = Convert.ToInt32(reader["IdMansioni"]);
                        u.Mansioni = m;
                        ListaUtenti.Add(u);
                    }
                }
            }catch(Exception ex){
              
                return null; }

            finally {
                sql.Close(); 
            }
            return ListaUtenti;

        }

        public static List<SelectListItem>UtenteSelect()
        {
           
               List<SelectListItem>listUtente= new List<SelectListItem>();
                SqlConnection sql=Shared.GetConnection();
                try
                {
                    sql.Open();
                    SqlCommand command = Shared.GetCommand("SELECT IDutente, Nome,Cognome from Utente", sql);
                  SqlDataReader reader= command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            SelectListItem selectUtente = new SelectListItem
                            {
                                Value = Convert.ToInt32(reader["IDutente"]).ToString(),
                                Text = reader["Nome"].ToString() + " " + reader["Cognome"].ToString()
                            };
                            listUtente.Add(selectUtente);
                        }
                    }
                }catch(Exception ex)
                {
                    return null;
                }
                finally { sql.Close(); }
                 return listUtente;
            
        }

        public static Utente GetUtente(int id)
        {
           
            SqlConnection sql = Shared.GetConnection();
            try {
                sql.Open();
                SqlCommand command = Shared.GetCommand("Select * from Utente where IDutente= @IDutente", sql);
                command.Parameters.AddWithValue("IDutente",id);
                SqlDataReader reader = command.ExecuteReader();


                 Utente u = new Utente();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        u.IDutente = Convert.ToInt32(reader["IDutente"]);
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

            return u;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                sql.Close();
            }
        }

        public static List<Utente> SearchName(string searchPhrase)
        {
            List<Utente> ListaUtenti = new List<Utente>();
            SqlConnection sql = Shared.GetConnection();
            try
            {
                sql.Open();
                SqlCommand command = Shared.GetCommand("select * from Utente inner join mansioni on utente.IdMansioni = Mansioni.IDmansioni where(Nome LIKE '%" + searchPhrase+ "%' or Cognome LIKE '%" + searchPhrase+ "%' or Descrizione LIKE '%" + searchPhrase+"%') ", sql);
               

                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       Mansioni m = new Mansioni();
                        m.Descrizione = reader["Descrizione"].ToString();
                        Utente u = new Utente();
                        u.IDutente = Convert.ToInt32(reader["IDutente"]);
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
                        u.Mansioni = m;
                        ListaUtenti.Add(u);
                    }
                }
            }
            catch (Exception ex)
            {

                return null;
            }

            finally
            {
                sql.Close();
            }
            return ListaUtenti;
        }
   

    }

    
}