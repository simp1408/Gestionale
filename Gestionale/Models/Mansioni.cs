using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Gestionale.Models
{
    public class Mansioni
    {
        public int IDmansioni { get; set; }

        [Display(Name = "Qualifica")]
        [Required(ErrorMessage = "La qualifica è obbligatoria")]
        public string Descrizione { get; set; }

        public static List<SelectListItem>MansioniSelect() { 
           
               List<SelectListItem>ListaMansioni= new List<SelectListItem>();

               SqlConnection sql = Shared.GetConnection(); 
           try {
                  sql.Open();

                  SqlCommand command = Shared.GetCommand("Select * from Mansioni",sql);

                    SqlDataReader reader= command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read()) {

                            SelectListItem selectMansioni = new SelectListItem
                            {
                               Value = reader["IDmansioni"].ToString(),
                                Text = reader["Descrizione"].ToString()
                            };

                            ListaMansioni.Add(selectMansioni); 
                        }
                        
                        
                    }

             }catch(Exception ex) {
                    
              return null; } 

               finally { 

                sql.Close();
             }

            return ListaMansioni;
        }

        
    }
}