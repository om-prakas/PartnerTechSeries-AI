using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Waterfallbot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        string Name;
        long PhoneNumber;
        string Companyname;


        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var response = await activity;

            PromptDialog.Text(
                context: context,
                resume: GetName,
                prompt: "Please enter your Name."
            );


        }

        public virtual async Task GetName(IDialogContext context, IAwaitable<string> Username)
        {
            string response = await Username;
            Name = response;

            PromptDialog.Text(
                context: context,
                resume: GetCompanyName,
                prompt: "Please enter your Company Name.",
                retry: "Sorry, I didn't understand that. Please try again."
            );
        }

        public virtual async Task GetCompanyName(IDialogContext context, IAwaitable<string> CompanyName)
        {
            string response = await CompanyName;
            Companyname= response; ;

            PromptDialog.Number(
                context: context,
                resume: GetPhoneNumber,
                prompt: "Please enter Phone Number.",
                retry: "Sorry, I didn't understand that. Please try again."
            );
        }
        public virtual async Task GetPhoneNumber(IDialogContext context, IAwaitable<long> PhoneNo)
        {
            long response = await PhoneNo;
            PhoneNumber =response;
            await context.PostAsync("Information Received:" + "\n\n"+ "Name : "+Name +"\n"+ "Company Name : "+Companyname+"\n"+"Phone Number: "+PhoneNumber);
            context.Done(this);
        }

       
        
    }
}