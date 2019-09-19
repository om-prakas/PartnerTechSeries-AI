using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HelloWorld.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Call(new RootLuisDialog(), this.ResumeAfter);

            return Task.CompletedTask;
        }

        private async Task ResumeAfter(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            
           
        }
    }
}