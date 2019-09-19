using AuthBot;
using AuthBot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;


namespace DevAuthBot.Dialogs
{
    [Serializable]
    public class AzureADDialog : IDialog<string>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        /// <summary>
        /// Login and Logout
        /// </summary>
        /// <param name="context"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            var message = await item as Activity;

            //endpoint v1
            if (string.IsNullOrEmpty(await context.GetAccessToken(ConfigurationManager.AppSettings["ActiveDirectory.ResourceId"])))
            {
                //Navigate to website for Login
                await context.Forward(new AzureAuthDialog(ConfigurationManager.AppSettings["ActiveDirectory.ResourceId"]), this.ResumeAfterAuth, message, CancellationToken.None);
            }
            else if(message.Text=="Exit")
            {
                //Logout
                await context.Logout();
                context.Wait(MessageReceivedAsync);
            }
        }

        /// <summary>
        /// ResumeAfterAuth
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task ResumeAfterAuth(IDialogContext context, IAwaitable<string> result)
        {
            //AD resposnse message 
            var message = await result;
            await context.PostAsync(message);
            await SuggestedAction(context, result);

            context.Wait(MessageReceivedAsync);
        }


        private async Task SuggestedAction(IDialogContext context, IAwaitable<object> result)
        {
            //var activity = await result as Activity;
            var message = context.MakeMessage();
            message.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                    {
                        new CardAction(){ Title = "Exit", Type=ActionTypes.ImBack, Value="Exit" }
                    }
            };

            await context.PostAsync(message);


        }








    }
}

