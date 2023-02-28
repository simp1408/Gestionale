using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Gestionale.Models
{
    public class Task
    {
        public int IDtask { get; set; }

        [Display(Name = "Descrizione Task")]
        [Required(ErrorMessage = "La descrizione è obbligatoria")]
        public string DescrizioneTask { get; set; }
        [Display(Name = "Inizio Task")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "La data di inizio è obbligatoria")]
        public DateTime DataInizio { get; set; }

        [Display(Name = "Scadenza Task")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "La data della scadenza è obbligatoria")]
        public DateTime DataScadenza { get; set; }

        [Display(Name = "Dipendente")]
        [Required(ErrorMessage = "nome dipendente è obbligatorio")]
        public int IdUtente { get; set; }

        [Display(Name = "Stato Task")]
        [Required(ErrorMessage = "stato task è obbligatorio")]
        public int IdStatoTask { get; set; }

        [Display(Name = "Priorità")]
        [Required(ErrorMessage = "La priorità è obbligatoria")]
        public int IdPriority { get; set; }

        public  Utente Utente { get; set; }
        public StatoTask StatoTask { get; set; }
        public string DescrizioneStato { get; set; }
        public Priority Priority { get; set; }

        public static List<Task> GetTask()
        {
            List<Task> listTask = new List<Task>();
            SqlConnection sql= Shared.GetConnection();
            try { 
                sql.Open();
                SqlCommand command = Shared.GetCommand("select * from Task inner join Utente on Task.IdUtente=Utente.IDutente " +
                     " inner join StatoTask on Task.IdStatoTask=StatoTask.IDstatoTask" +
                     " inner join Priority on Task.IdPriority=Priority.IDpriority", sql);
                SqlDataReader reader= command.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Task t = new Task();
                        Utente u= new Utente();
                        u.Nome = reader["Nome"].ToString();
                        u.Cognome = reader["Cognome"].ToString() ;
                        StatoTask stask= new StatoTask();
                        stask.Stato = reader["Stato"].ToString();
                        Priority pr= new Priority();
                        pr.LevelPriority = reader["LevelPriority"].ToString();

                        t.IDtask = Convert.ToInt32(reader["IDtask"]);
                        t.DescrizioneTask = reader["DescrizioneTask"].ToString();
                        t.DataInizio = Convert.ToDateTime(reader["DataInizio"]);
                        t.DataScadenza = Convert.ToDateTime(reader["DataScadenza"]);
                        t.IdUtente = Convert.ToInt32(reader["IdUtente"]);
                        t.IdStatoTask = Convert.ToInt32(reader["IdStatoTask"]);
                        t.IdPriority = Convert.ToInt32(reader["IdPriority"]);
                        t.Utente = u;
                        t.StatoTask= stask;
                        t.Priority= pr;
                        listTask.Add(t);
                    }
                    
                }
    
            }catch(Exception ex) { 
                return null; } 
            finally { sql.Close(); }
            
            return listTask;
        }

        public static List<Task> GetUserTasks(int id)
        {
            List<Task> listUserTask = new List<Task>();
            SqlConnection sql = Shared.GetConnection();
            try
            {
                sql.Open();
                
                SqlCommand command = Shared.GetCommand("select * from Task inner join Utente on Task.IdUtente=Utente.IDutente " +
                     " inner join StatoTask on Task.IdStatoTask=StatoTask.IDstatoTask" +
                     " inner join Priority on Task.IdPriority=Priority.IDpriority where Utente.IDutente=@IDutente", sql);

                command.Parameters.AddWithValue("@IDutente", id);
                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Utente u = new Utente();
                        Task t = new Task();

                        u.IDutente = Convert.ToInt32(reader["IDutente"]);
                        u.Nome = reader["Nome"].ToString();
                        u.Cognome = reader["Cognome"].ToString();
                        u.Username = u.Username;
                        StatoTask stask = new StatoTask();
                        stask.Stato = reader["Stato"].ToString();
                        Priority pr = new Priority();
                        pr.LevelPriority = reader["LevelPriority"].ToString();
                        t.IDtask = Convert.ToInt32(reader["IDtask"]);
                        t.DescrizioneTask = reader["DescrizioneTask"].ToString();
                        t.DataInizio = Convert.ToDateTime(reader["DataInizio"]);
                        t.DataScadenza = Convert.ToDateTime(reader["DataScadenza"]);
                        t.IdUtente = Convert.ToInt32(reader["IdUtente"]);
                        t.IdStatoTask = Convert.ToInt32(reader["IdStatoTask"]);
                        t.IdPriority = Convert.ToInt32(reader["IdPriority"]);
                        t.Utente = u;
                        t.StatoTask = stask;
                        t.Priority = pr;
                        listUserTask.Add(t);

                    }

                }
               

            }
            catch (Exception ex)
            {
                return null;
            }
            finally { sql.Close(); }
            
            return listUserTask;


        }

        //metodo che mi da restituisce id dell username
        public static int GetUserId(String username)
        {
            SqlConnection sql = Shared.GetConnection();
            try
            {
                sql.Open();
                SqlCommand command = Shared.GetCommand("Select IDutente from Utente where Username=@Username", sql);
                command.Parameters.AddWithValue("@Username",username);
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       
                        return Convert.ToInt32(reader["IDutente"]);

                    }
                }
                return 0;


            }
            catch (Exception ex)
            {

                return 0;
            }
            finally
            {
                sql.Close();
            }

        }

        public static Task EditStato(Task t,StatoTask Stato)
        {
            SqlConnection sql = Shared.GetConnection();
        
            t.DescrizioneStato = Stato.Stato;
            t.IdStatoTask = Stato.IDstatoTask;
            try
            {
                sql.Open();
                SqlCommand command = Shared.GetCommand("UPDATE Task SET IdStatoTask = CASE IDtask WHEN @IDtask THEN @IdStatoTask ELSE IdStatoTask END WHERE IDtask IN (@IDtask)", sql);
                command.Parameters.AddWithValue("@IDtask", t.IDtask);
                //command.Parameters.AddWithValue("@IdStatoTask", t.IdStatoTask);
                command.Parameters.AddWithValue("@IdStatoTask", t.IdStatoTask);

                int row = command.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                sql.Close();
            }

            return t;
        }

        public static Task GetOneTask(int id)
        {
           
            SqlConnection sql = Shared.GetConnection();
            try
            {
                sql.Open();

                SqlCommand command = Shared.GetCommand("select * from Task inner join Utente on Task.IdUtente=Utente.IDutente " +
                     " inner join StatoTask on Task.IdStatoTask=StatoTask.IDstatoTask" +
                     " inner join Priority on Task.IdPriority=Priority.IDpriority where Utente.IDutente=@IDutente", sql);

                command.Parameters.AddWithValue("@IDutente", id);
                SqlDataReader reader = command.ExecuteReader();

                Task t = new Task();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Utente u = new Utente();

                        u.IDutente = Convert.ToInt32(reader["IDutente"]);
                        u.Nome = reader["Nome"].ToString();
                        u.Cognome = reader["Cognome"].ToString();
                        u.Username = u.Username;
                        StatoTask stask = new StatoTask();
                        stask.Stato = reader["Stato"].ToString();
                        Priority pr = new Priority();
                        pr.LevelPriority = reader["LevelPriority"].ToString();
                        t.IDtask = Convert.ToInt32(reader["IDtask"]);
                        t.DescrizioneTask = reader["DescrizioneTask"].ToString();
                        t.DataInizio = Convert.ToDateTime(reader["DataInizio"]);
                        t.DataScadenza = Convert.ToDateTime(reader["DataScadenza"]);
                        t.IdUtente = Convert.ToInt32(reader["IdUtente"]);
                        t.IdStatoTask = Convert.ToInt32(reader["IdStatoTask"]);
                        t.IdPriority = Convert.ToInt32(reader["IdPriority"]);
                        t.Utente = u;
                        t.StatoTask = stask;
                        t.DescrizioneStato=stask.Stato.ToString();
                        t.Priority = pr;
                       

                    }

                }
            return t;


            }
            catch (Exception ex)
            {
                return null;
            }
            finally { sql.Close(); }



        }






    }
}