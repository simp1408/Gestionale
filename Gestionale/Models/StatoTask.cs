using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Gestionale.Models
{
    public class StatoTask
    {
        public int IDstatoTask { get; set; }

        [Display(Name = "Stato Task")]
        [Required(ErrorMessage = "Lo stato della task è obbligatorio")]
        public string Stato { get; set; }

        public static List<SelectListItem>StatoTaskSelect() { 
             

                List<SelectListItem>ListStatoTask= new List<SelectListItem>();
                SqlConnection sql = Shared.GetConnection();
                try { 
                    sql.Open();

                    SqlCommand command = Shared.GetCommand("select * from StatoTask",sql);
                    SqlDataReader reader= command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            SelectListItem statoTaskSelect = new SelectListItem
                            {
                                Value =reader["IDstatoTask"].ToString(),
                                Text= reader["Stato"].ToString()
                            };

                            ListStatoTask.Add(statoTaskSelect);
                        }
                    }
                
                }catch(Exception ex) { 
                    return null; }
                finally { sql.Close(); }

            
                 return ListStatoTask;
            
        }

        public static StatoTask GetStato(int id) {

            SqlConnection sql = Shared.GetConnection();
            try {
                sql.Open();
                SqlCommand command = Shared.GetCommand("Select * from StatoTask where IDstatoTask=@IDstatoTask", sql);
                command.Parameters.AddWithValue("@IDstatoTask", id);
                SqlDataReader reader= command.ExecuteReader();
                  StatoTask st= new StatoTask();
               
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        st.IDstatoTask = id;
                        st.Stato = reader["Stato"].ToString();

                    }
                   
                }
                
                return st;   
                    
                    
                    }catch(Exception ex) {
                return null; 
            } 
            finally { 
                sql.Close();
            }
            

        }
    }
}