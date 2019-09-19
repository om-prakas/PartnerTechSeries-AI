namespace MiddlewareBot
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.History;
    using Microsoft.Bot.Connector;
    using System.IO;

#pragma warning disable 1998

    public class DebugActivityLogger : IActivityLogger
    {
        public async Task LogAsync(IActivity activity)
        {
            
            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter("C:\\Users\\Siddharth Dubey\\source\\repos\\Botv3\\FileLoggerBot\\LogData.txt");

                //Write a line of text
                sw.WriteLine($"From:{activity.From.Id} - To:{activity.Recipient.Id} - Message:{activity.AsMessageActivity()?.Text}");

                

                //Close the file
                sw.Close();
            }
            catch (System.Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Debug.WriteLine("Executing finally block.");
            }

        }
    }
}
