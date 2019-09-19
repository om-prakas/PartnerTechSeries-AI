using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Web.Configuration;

namespace QnAMakerBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {

        
        

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            string HostName = WebConfigurationManager.AppSettings["HostName"];
            
            string kb = WebConfigurationManager.AppSettings["kb"];
            string service = "/qnamaker";
           
            string uri = HostName + "/knowledgebases/" + kb + "/generateAnswer";
            string question = @"{'question':'"+activity.Text+"','top': 3}";
            var response = await Post(uri, question);
            dynamic httpResponseJson = JsonConvert.DeserializeObject(response);
            var replyMessage = (string)httpResponseJson.answers[0].answer;
            if (replyMessage == "No good match found in KB.")
            {
                replyMessage = "I didn't get it.I am still learning.";
                await context.PostAsync(replyMessage);
            }
            else {
                await context.PostAsync(replyMessage);
            }
            
            context.Wait(MessageReceivedAsync);
        }

        async static Task<string> Post(string uri, string body)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                string endpoint_key = WebConfigurationManager.AppSettings["endpoint_key"];
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", "EndpointKey " + endpoint_key);

                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }


    }
}