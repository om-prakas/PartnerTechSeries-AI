using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

using System.Net.Http;

using System.Web.Configuration;

namespace HelloWorld.Dialogs
{
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {
        public RootLuisDialog() : base(GetNewService())
        {

        }

        private static ILuisService[] GetNewService()
        {
            var modelId = WebConfigurationManager.AppSettings["LuisApplicationID"];
            var subscriptionKey = WebConfigurationManager.AppSettings["LuisEndPointKey"];

            var luisModel = new LuisModelAttribute(modelId, subscriptionKey);
            return new ILuisService[] { new LuisService(luisModel) };
        }


        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"I didn't get it.I am still learning.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }


        [LuisIntent("Intent1")]
        public async Task GetIntentOne(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
           
            await context.PostAsync("Intent 1 is encountered.");



        }

        [LuisIntent("Intent2")]
        public async Task GetIntentTwo(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync("Intent 2 is encountered.");

        }


        [LuisIntent("Intent3")]
        public async Task GetIntentThree(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Intent 3 is encountered");

        }

     

    }
}