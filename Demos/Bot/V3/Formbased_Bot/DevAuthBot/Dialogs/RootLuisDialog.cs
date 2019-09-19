
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;
using System.Web.Hosting;
using AdaptiveCards;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using Microsoft.Bot.Builder.ConnectorEx;




namespace DevAuthBot.Dialogs
{
   
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {

        public RootLuisDialog() : base(GetNewService())
        {

        }

        private static ILuisService[] GetNewService()
        {
            var modelId = ConfigurationManager.AppSettings.Get("LuisModelId");
            var subscriptionKey = ConfigurationManager.AppSettings.Get("LuisSubscriptionKey");
          
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


        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            var message = await item;

            dynamic value = message.Value;
            string tex = value["id"];
            if (value != null) {

                if (tex == "submit_forgotpassword") {

                    string dataInput1 = value["FP_userid"];
                    string dataInput2 = value["FP_mobilenumber"];
                    string dataInput3 = value["FP_emailid"];

                    if ((dataInput1 == "") || (dataInput2 == "") || (dataInput3 == ""))
                    {
                        await context.PostAsync($"Please fill all fields above.");
                        
                    }
                    else
                    {
                        await context.PostAsync($"Request for Physical Password has been registered successfully.\nIt will reach your registered Address within 8 working days.");
                        
                        await SuggestiveActions(context, item);
                        context.Wait(this.MessageReceived);



                    }

                    

                }
                else if (tex == "submit_forgotpassword_cancel") {

                    await SuggestiveActions(context, item);
                    context.Wait(this.MessageReceived);

                } else if (tex == "submit_others") {
                    string data = value["data_others"];
                    if (data == "")
                    {
                        await context.PostAsync($"Please fill all fields above.");
                    }
                    else
                    {
                        await context.PostAsync($"Your Complaint is Registered with us successfully.\nPlease note down Reference Number :HG1278 for further Reference.");
                        await SuggestiveActions(context, item);
                        context.Wait(this.MessageReceived);
                    }
                } else if (tex == "submit_others_cancel") {

                    await SuggestiveActions(context, item);
                    context.Wait(this.MessageReceived);
                } else if (tex == "submit_otp")
                {
                    string data = value["otp_data"];
                    if (data == "")
                    {
                        await context.PostAsync($"Please fill all fields above.");

                    }
                    else
                    {
                        await context.PostAsync($"Your Mobile Number for OTP is registered successfully.Please note down Reference Number :HG1278 for further Reference.");
                        await SuggestiveActions(context, item);
                        context.Wait(this.MessageReceived);
                    }

                } else if (tex == "submit_otp_cancel")
                {
                    await SuggestiveActions(context, item);
                    context.Wait(this.MessageReceived);

                } else if (tex == "submit_checkbook")
                {
                    string data = value["data_checkbook"];
                    if (data == "")
                    {
                        await context.PostAsync($"Please fill all fields above.");

                    }
                    else
                    {
                        await context.PostAsync($"Your Request has been successfully Registered.Please note down Reference Number :HG1278 for further Reference.\nCheck Book will be delivered to Registered Address within 8 working days.");
                        await SuggestiveActions(context, item);
                        context.Wait(this.MessageReceived);

                    }

                }
                else if (tex == "submit_checkbook_cancel")
                {

                    await SuggestiveActions(context, item);
                    context.Wait(this.MessageReceived);

                }
                else if (tex == "submit_updatemobileno")
                {
                    string data = value["data_updatemobileno"];
                    if (data == "")
                    {
                        await context.PostAsync($"Please fill all fields above.");
                    }
                    else
                    {
                        await context.PostAsync($"Your Mobile Number has been updated successfully.Please note down Reference Number :HG1278 for further Reference.");
                        await SuggestiveActions(context, item);
                        context.Wait(this.MessageReceived);
                       

                    }

                } else if (tex == "submit_updatemobileno_cancel")
                {

                    await SuggestiveActions(context, item);
                    context.Wait(this.MessageReceived);

                }


            }


        }

        [LuisIntent("HandleYelling")]
        public async Task HandleYelling(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Oops!Sorry to hear that.I am still learning and hoping to get better soon.");
            await SuggestiveActions(context, activity);

        }

        [LuisIntent("Definepersona")]
        public async Task Definepersona(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"I was created by a group of Data Scientists.");
            await SuggestiveActions(context, activity);

        }

        [LuisIntent("HandleBadGestures")]
        public async Task HandleBadGestures(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Sorry,I am not here to do that.");
            await SuggestiveActions(context, activity);

        }

        [LuisIntent("HandleGender")]
        public async Task HandleGender(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"I am gender agnostic.");
            await SuggestiveActions(context, activity);

        }

        [LuisIntent("HandleGoodGestures")]
        public async Task HandleGoodGestures(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Thanks for being so kind.");
            await SuggestiveActions(context, activity);

        }






        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Welcome to SBI Bank.How can I help you?");
            await SuggestiveActions(context, activity);

        }

        [LuisIntent("Account Balance")]
        public async Task AccountBalance(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Customer Name : Mr. Srikar Kumar\nAccount No :7893564521\nAccount Balance :50000 Rs.");
            await SuggestiveActions(context, activity);
        }

        [LuisIntent("CardBlock_Complaint")]
        public async Task CardBlock_Complaint(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Your Request has been generated for Blocking Card No:XXXXX XXXXXXXXXXX 152.\nPlease note down Complaint Reference Number :435G34 for further Reference.");
            await SuggestiveActions(context, activity);
        }


        [LuisIntent("CheckBook_RequestToBank")]
        public async Task CheckBook_RequestToBank(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string filePath = HostingEnvironment.MapPath($"/Adaptive_Cards/CheckBookRequest.json");
            var adaptiveCardJson = File.ReadAllText(filePath);
            var returnMessage = context.MakeMessage();
            var results = AdaptiveCard.FromJson(adaptiveCardJson);
            var card = results.Card;
            returnMessage.Attachments.Add(new Attachment()
            {
                Content = card,
                ContentType = "application/vnd.microsoft.card.adaptive",
                Name = "Card"
            });
            await context.PostAsync(returnMessage);
            context.Wait(MessageReceivedAsync);


        }


        [LuisIntent("Complaint")]
        public async Task Complaint(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Please register your complaint here.");
            await SuggestiveComplaint(context, activity);
        }

        [LuisIntent("Exit")]
        public async Task Exit(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            
            context.Done(new AzureADDialog().MessageReceivedAsync(context,activity));
            
        }

        [LuisIntent("Competitor")]
        public async Task Competitor(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"No,I am different.");
        }









        [LuisIntent("ForgotPassword_Complaint")]
        public async Task ForgotPassword_Complaint(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            string filePath = HostingEnvironment.MapPath($"/Adaptive_Cards/ForgotPassword.json");
            var adaptiveCardJson = File.ReadAllText(filePath);
            var returnMessage = context.MakeMessage();
            var results = AdaptiveCard.FromJson(adaptiveCardJson);
            var card = results.Card;
            returnMessage.Attachments.Add(new Attachment()
            {
                Content = card,
                ContentType = "application/vnd.microsoft.card.adaptive",
                Name = "Card",
            });
            await context.PostAsync(returnMessage);
            context.Wait(MessageReceivedAsync);
            

        }

        [LuisIntent("ForgotPIN_Complaint")]
        public async Task ForgotPIN_Complaint(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Please Check the Registered E-mail ID for a link sent by us in email.");
            await SuggestiveActions(context, activity);

        }

        [LuisIntent("Nearest ATM")]
        public async Task Nearest_ATM(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"I found 4 Nearest ATM.");
            var reply = context.MakeMessage();

            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = GetCardsAttachmentsForATM();

            await context.PostAsync(reply);
            await SuggestiveActions(context, activity);

        }

        private static IList<Attachment> GetCardsAttachmentsForATM()
        {
            return new List<Attachment>()
            {
                GetHeroCard(
                    "1 Km from Current Location.",
                    "No.150, Velachery Tambaram Rd, V.G.P Pushpa Nagar, Medavakkam, Chennai, Tamil Nadu 600100",
                    new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "View Map", value: "https://www.google.com/maps/dir/12.9137106,80.1819266/State+Bank+of+India,+No.150,+Velachery+Tambaram+Rd,+V.G.P+Pushpa+Nagar,+Medavakkam,+Chennai,+Tamil+Nadu+600100/@12.9172299,80.1789818,16z/data=!3m1!4b1!4m9!4m8!1m1!4e1!1m5!1m1!1s0x3a525c1fffa58599:0x2901962e5eeaf17!2m2!1d80.1875752!2d12.9193671")),
                GetHeroCard(
                    "700 metre from Current Location.",
                    "3/153, 11/329, Perumbakkam Main Rd, Opp ARUN Hospital, Medavakkam, Chennai, Tamil Nadu 600100",
                    new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "View Map", value: "https://www.google.com/maps/dir//nearest+sbi+atm/data=!4m6!4m5!1m1!4e2!1m2!1m1!1s0x3a525c1f1e55d089:0x9c58792bc66efa35?sa=X&ved=2ahUKEwi5w_v10v7hAhXm7HMBHb34ABUQ9RcwAHoECAEQCQ")),
                GetHeroCard(
                    "2 Km from Current Location",
                    "Shop No. 94, Velachery Main Rd, Pallikaranai, Chennai, Tamil Nadu 600100",
                    new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "View Map", value: "https://www.google.com/maps/dir//nearest+sbi+atm/data=!4m6!4m5!1m1!4e2!1m2!1m1!1s0x3a525c1878a5398b:0x422c12ef418ce4d9?sa=X&ved=2ahUKEwiQyP7A0_7hAhXzjuYKHcp5CrkQ9RcwAHoECAEQCA")),
                GetHeroCard(
                    "100 Metre from Current Location",
                    "Munusamy Nagar, Vimala Nagar, Medavakkam, Chennai, Tamil Nadu 600100",
                    new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "View Map", value: "https://www.google.com/maps/dir//nearest+sbi+atm/data=!4m6!4m5!1m1!4e2!1m2!1m1!1s0x3a525c1fab940a2b:0x289cd69a4db77f3?sa=X&ved=2ahUKEwiu18y_0_7hAhV1muYKHULKDLsQ9RcwAHoECAEQCA")),
            };
        }
















        [LuisIntent("Nearest Branch")]
        public async Task Nearest_Branch(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"I found 4 Nearest Branch.");
            var reply = context.MakeMessage();

            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = GetCardsAttachmentsForBranch();

            await context.PostAsync(reply);
            await SuggestiveActions(context, activity);

        }

        private static IList<Attachment> GetCardsAttachmentsForBranch()
        {
            return new List<Attachment>()
            {
                GetHeroCard(
                    "1 Km from Current Location.",
                    "No.150, Velachery Tambaram Rd, V.G.P Pushpa Nagar, Medavakkam, Chennai, Tamil Nadu 600100",
                    new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "View Map", value: "https://www.google.com/maps/dir/12.9137106,80.1819266/State+Bank+of+India,+No.150,+Velachery+Tambaram+Rd,+V.G.P+Pushpa+Nagar,+Medavakkam,+Chennai,+Tamil+Nadu+600100/@12.9172299,80.1789818,16z/data=!3m1!4b1!4m9!4m8!1m1!4e1!1m5!1m1!1s0x3a525c1fffa58599:0x2901962e5eeaf17!2m2!1d80.1875752!2d12.9193671")),
                GetHeroCard(
                    "700 metre from Current Location.",
                    "3/153, 11/329, Perumbakkam Main Rd, Opp ARUN Hospital, Medavakkam, Chennai, Tamil Nadu 600100",
                    new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "View Map", value: "https://www.google.com/maps/dir//nearest+sbi+atm/data=!4m6!4m5!1m1!4e2!1m2!1m1!1s0x3a525c1f1e55d089:0x9c58792bc66efa35?sa=X&ved=2ahUKEwi5w_v10v7hAhXm7HMBHb34ABUQ9RcwAHoECAEQCQ")),
                GetHeroCard(
                    "2 Km from Current Location",
                    "Shop No. 94, Velachery Main Rd, Pallikaranai, Chennai, Tamil Nadu 600100",
                    new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "View Map", value: "https://www.google.com/maps/dir//nearest+sbi+atm/data=!4m6!4m5!1m1!4e2!1m2!1m1!1s0x3a525c1878a5398b:0x422c12ef418ce4d9?sa=X&ved=2ahUKEwiQyP7A0_7hAhXzjuYKHcp5CrkQ9RcwAHoECAEQCA")),
                GetHeroCard(
                    "100 Metre from Current Location",
                    "Munusamy Nagar, Vimala Nagar, Medavakkam, Chennai, Tamil Nadu 600100",
                    new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "View Map", value: "https://www.google.com/maps/dir//nearest+sbi+atm/data=!4m6!4m5!1m1!4e2!1m2!1m1!1s0x3a525c1fab940a2b:0x289cd69a4db77f3?sa=X&ved=2ahUKEwiu18y_0_7hAhV1muYKHULKDLsQ9RcwAHoECAEQCA")),
            };
        }

        private static Attachment GetHeroCard(string title, string subtitle, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }





        [LuisIntent("Offers")]
        public async Task Offers(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"I found 4 Offers");

            var reply = context.MakeMessage();

            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = GetCardsAttachmentsForOffers();

            await context.PostAsync(reply);
            await SuggestiveActions(context, activity);

        }



        private static IList<Attachment> GetCardsAttachmentsForOffers()
        {
            return new List<Attachment>()
            {
                GetHeroCard(
                    "Vijay Sales",
                    "5% Cashback at vijay Sales.",
                    new CardImage(url: "https://www.sbicard.com/sbi-card-en/assets/media/images/personal/offers/categories/shopping/vijaysales/thumb-vijay-sales.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "View Offer", value: "https://www.sbicard.com/en/personal/offer/vijaysales.page")),
                GetHeroCard(
                    "spencers",
                    "Get 5% Cashback.",
                    new CardImage(url: "https://www.sbicard.com/sbi-card-en/assets/media/images/personal/offers/spencers/thumb-spencer.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "View Offer", value: "https://www.sbicard.com/en/personal/offer/spencers.page")),
                GetHeroCard(
                    "Healthians",
                    "Flat 50% off on Healthians SAVE HEALTH PACKAGE",
                    new CardImage(url: "https://www.sbicard.com/sbi-card-en/assets/media/images/personal/offers/categories/lifestyle/healthians/thumb-healthians.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "View Offer", value: "https://www.sbicard.com/en/personal/offer/healthians.page")),
                GetHeroCard(
                    "Amazon",
                    "10% Instant Discount at Amazon.in",
                    new CardImage(url: "https://www.sbicard.com/sbi-card-en/assets/media/images/personal/offers/categories/shopping/amazon-summer-sale/thumb-amazon-summer-sale.jpg"),
                    new CardAction(ActionTypes.OpenUrl, "View Offer", value: "https://www.sbicard.com/en/personal/offer/amazon-summer-sale.page")),
            };
        }






        [LuisIntent("Others_Complaint")]
        public async Task Others_Complaint(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            string filePath = HostingEnvironment.MapPath($"/Adaptive_Cards/OthersComplaint.json");
            var adaptiveCardJson = File.ReadAllText(filePath);
            var returnMessage = context.MakeMessage();
           
            var results = AdaptiveCard.FromJson(adaptiveCardJson);
            var card = results.Card;
            returnMessage.Attachments.Add(new Attachment()
            {
                Content = card,
                ContentType = "application/vnd.microsoft.card.adaptive",
                Name = "Card"
            });
            await context.PostAsync(returnMessage);
            context.Wait(MessageReceivedAsync);

        }

        [LuisIntent("OTP_RequestToBank")]
        public async Task OTP_RequestToBank(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;


            string filePath = HostingEnvironment.MapPath($"/Adaptive_Cards/OtpRequest.json");
            var adaptiveCardJson = File.ReadAllText(filePath);
            var returnMessage = context.MakeMessage();
            var results = AdaptiveCard.FromJson(adaptiveCardJson);
            var card = results.Card;
            returnMessage.Attachments.Add(new Attachment()
            {
                Content = card,
                ContentType = "application/vnd.microsoft.card.adaptive",
                Name = "Card"
            });
            await context.PostAsync(returnMessage);
            context.Wait(MessageReceivedAsync);

        }


      

        [LuisIntent("Recent Transactions")]
        public async Task Recent_Transactions(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            string filePath = HostingEnvironment.MapPath($"/Adaptive_Cards/RecentTransection.json");
            var adaptiveCardJson = File.ReadAllText(filePath);
            var returnMessage = context.MakeMessage();
            var results = AdaptiveCard.FromJson(adaptiveCardJson);
            var card = results.Card;
            returnMessage.Attachments.Add(new Attachment()
            {
                Content = card,
                ContentType = "application/vnd.microsoft.card.adaptive",
                Name = "Card"
            });
            await context.PostAsync(returnMessage);
            await SuggestiveActions(context, activity);

        }

        [LuisIntent("Request_to_Bank")]
        public async Task Request_to_Bank(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Please register your Request to Bank.");
            SuggestiveRequestToBank(context, activity);


        }

        [LuisIntent("UpdateMobileNo_RequestToBank")]
        public async Task UpdateMobileNo_RequestToBank(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;


            string filePath = HostingEnvironment.MapPath($"/Adaptive_Cards/UpdateMobileNumber.json");
            var adaptiveCardJson = File.ReadAllText(filePath);
            var returnMessage = context.MakeMessage();
            var results = AdaptiveCard.FromJson(adaptiveCardJson);
            var card = results.Card;
            returnMessage.Attachments.Add(new Attachment()
            {
                Content = card,
                ContentType = "application/vnd.microsoft.card.adaptive",
                Name = "Card"
            });
            await context.PostAsync(returnMessage);
            context.Wait(MessageReceivedAsync);

        }

        private async Task SuggestiveActions(IDialogContext context, IAwaitable<object> result)
        {
            
            var message = context.MakeMessage();
            message.SuggestedActions = new SuggestedActions()
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

        private async Task SuggestiveComplaint(IDialogContext context, IAwaitable<object> result)
        {
            
            var message = context.MakeMessage();
            message.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                    {
                        new CardAction(){ Title = "Card Block", Type=ActionTypes.ImBack, Value="Card Block" },
                        new CardAction(){ Title = "Forgot PIN", Type=ActionTypes.ImBack, Value="Forgot PIN"},
                        new CardAction(){ Title = "Forgot Password", Type=ActionTypes.ImBack, Value="Forgot Password"},
                        new CardAction(){ Title = "Others", Type=ActionTypes.ImBack, Value="Others" }
                        
                    }
            };

            await context.PostAsync(message);


        }


        private async Task SuggestiveRequestToBank(IDialogContext context, IAwaitable<object> result)
        {
            
            var message = context.MakeMessage();
            message.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                    {
                        new CardAction(){ Title = "OTP", Type=ActionTypes.ImBack, Value="OTP" },
                        new CardAction(){ Title = "Checkbook", Type=ActionTypes.ImBack, Value="Checkbook"},
                        new CardAction(){ Title = "Update Mobile Number", Type=ActionTypes.ImBack, Value="Update Mobile Number"}
                      

                    }
            };

            await context.PostAsync(message);


        }











    }
}