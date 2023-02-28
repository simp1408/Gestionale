using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Gestionale.Models
{
    public class Priority
    {
        public int IDpriority { get; set; }
        [Display(Name = "Livello Priorità")]
        [Required(ErrorMessage = "Livello di priorità è obbligatorio")]
        public string LevelPriority { get; set; }

      public static List<SelectListItem> PrioritySelect() {
            
                List<SelectListItem> listPriority = new List<SelectListItem>();
                
                SqlConnection sql = Shared.GetConnection();
                try{ 
                     sql.Open();
                    SqlCommand command = Shared.GetCommand("Select * from Priority",sql);
                    
                    SqlDataReader reader= command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            SelectListItem selectPriority = new SelectListItem
                            {
                                Value = Convert.ToInt32(reader["IDpriority"]).ToString(),
                                Text = reader["LevelPriority"].ToString()
                            };
                            listPriority.Add(selectPriority);
                        }
                    }

                }catch(Exception ex) { 
                    
                    return null; } 
                
                finally { sql.Close(); 
                    }

                return listPriority;
       }
          
        
        
        
        
    }
}