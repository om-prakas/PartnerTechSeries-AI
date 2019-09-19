namespace MiddlewareBot
{

    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.History;
    using Microsoft.Bot.Connector;
    using System.Data.SqlClient;
    using System;
    
    using System.Text;
    using System.Web.Configuration;

#pragma warning disable 1998

    public class DebugActivityLogger : IActivityLogger
    {
        public async Task LogAsync(IActivity activity)
        {
            string fromid= activity.From.Id;
            string toid= activity.Recipient.Id;
            string message= activity.AsMessageActivity()?.Text;
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = WebConfigurationManager.AppSettings["DataSource"];
                builder.UserID = WebConfigurationManager.AppSettings["UserID"];
                builder.Password = WebConfigurationManager.AppSettings["Password"];
                builder.InitialCatalog = WebConfigurationManager.AppSettings["InitialCatalog"];


               

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Insert into user_chat_log(from_id,to_id,message)values('"+fromid + "','"+ toid + "','"+ message+ "')");
                  
                    

                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                                Debug.WriteLine("Insert in Azure SQL DataBase Successfull");
                                
                            
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine("Error is there :"+e.ToString());
            }

            
        }
    }
}
