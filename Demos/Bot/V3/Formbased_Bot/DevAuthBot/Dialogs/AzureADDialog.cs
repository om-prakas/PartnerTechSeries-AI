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
using Microsoft.Bot.Builder.ConnectorEx;

namespace DevAuthBot.Dialogs
{
    [Serializable]
    
    public class AzureADDialog : IDialog<object>
      
    {
        
        
        

        public async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();

            
            var heroCard = new HeroCard
            {
                Title = "State Bank of India",
                Subtitle = "The Banker of every Indian.",
                Text = "This platform will help you to find your daily Banking needs.",
                Images = new List<CardImage> { new CardImage("https://content3.jdmagicbox.com/comp/mumbai/97/022p8005997/catalogue/state-bank-of-india-head-office--bandra-kurla-complex-bandra-east-mumbai-state-bank-of-india-6839l-250.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Know More", value: "https://www.sbi.co.in/") }
            };
            var attachment = heroCard.ToAttachment();

            message.Attachments.Add(attachment);
            

            await context.PostAsync(message);
            context.Wait(MessageReceivedAsync);
        }

      
        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            var message = await item;
          

            
            if (string.IsNullOrEmpty(await context.GetAccessToken(ConfigurationManager.AppSettings["ActiveDirectory.ResourceId"])))
            {


                
                await context.Forward(new AzureAuthDialog(ConfigurationManager.AppSettings["ActiveDirectory.ResourceId"]), this.ResumeAfterAuth, message, CancellationToken.None);
               
                }
            else
            {
                //Logout
                await context.Logout();
                context.Wait(MessageReceivedAsync);
            }
        }

    
        private async Task ResumeAfterAuth(IDialogContext context, IAwaitable<string> result)
        {
            
            var message = await result;
          
           await context.PostAsync(message);


           
            await context.PostAsync($"How can I help you ?");
            await SuggestiveActions(context,result);

           context.Call(new RootLuisDialog(), this.ResumeAfter);
           

        }

        private async Task ResumeAfterNewOrderDialog(IDialogContext context, IAwaitable<IEventActivity> result)
        {
            
            var resultFromNewOrder = await result;

            await context.PostAsync($"New order dialog just told me this: {resultFromNewOrder}");

            

        }

        private async Task SuggestiveActions(IDialogContext context, IAwaitable<object> result)
        {
            
            var message = context.MakeMessage();
            message.SuggestedActions=new SuggestedActions()
            {
                Actions = new List<CardAction>()
                    {
                        new CardAction(){ Title = "Account Balance", Type=ActionTypes.ImBack, Value="Account Balance" },
                        new CardAction(){ Title = "Recent Transactions", Type=ActionTypes.ImBack, Value="Recent Transactions" },
                        new CardAction(){ Title = "Nearest Branch", Type=ActionTypes.ImBack, Value="Nearest Branch" },
                        new CardAction(){ Title = "Nearest ATM", Type=ActionTypes.ImBack, Value="Nearest ATM" },
                        new CardAction(){ Title = "Complaint", Type=ActionTypes.ImBack, Value="Complaint" },
                        new CardAction(){ Title = "Offers", Type=ActionTypes.ImBack, Value="Offers" },
                        new CardAction(){ Title = "Request to Bank", Type=ActionTypes.ImBack, Value="Request to Bank" },
                        new CardAction(){ Title = "Exit", Type=ActionTypes.ImBack, Value="Exit" }
                    }
            };

            await context.PostAsync(message);
           

        }

        private async Task ResumeAfter(IDialogContext context, IAwaitable<object> result)
        {
           
            var resultFromNewOrder = await result;

            

        }


    }
}

