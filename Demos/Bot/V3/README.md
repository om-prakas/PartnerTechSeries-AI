## Deployment Guide ##

This document is a step by step guide to deploy C# Microsoft Bot Framework V3 Chatbot on Microsoft Azure as a service for different communication channel.   

### Steps for Deployment : ###


- Login to your [Azure Account](http://portal.azure.com).

![frontPage](https://user-images.githubusercontent.com/48123984/57964875-ed316b80-7959-11e9-83d1-f5f9c5fba113.jpg)

- Please follow the following steps to reach 'Web App Bot' Section in Microsoft Azure :
  - Click on 'Create a resource' in left side Menu .
  - Click on 'AI+Machine Learning' in 'Azure Marketplace' menu of New Section.
  - Click on 'Web App Bot' in Featured section for 'AI+Machine Learning'.

![Image2](https://user-images.githubusercontent.com/48123984/57964955-f7079e80-795a-11e9-9237-00a86c379998.jpg)

- Fill all Details in Web App Bot Section.
   - Description of each detail is given below :

Setting	| Suggested value | Description |
| -- | -- | -- |
Bot name |Your bot's display name|	The display name for the bot that appears in channels and directories. This name can be changed at anytime.|
Subscription	|Your subscription|	Select the Azure subscription you want to use.|
Resource Group |	myResourceGroup |	You can create a new resource group or choose from an existing one.|
Location |	The default location	| Select the geographic location for your resource group. Your location choice can be any location listed, though it's often best to choose a location closest to your customer. The location cannot be changed once the bot is created.|
Pricing tier |	F0 |	Select a pricing tier. You may update the pricing tier at any time. For more information, see Bot Service pricing.|
App name  |	A unique name |	The unique URL name of the bot. For example, if you name your bot myawesomebot, then your bot's URL will be http://myawesomebot.azurewebsites.net. The name must use alphanumeric and underscore characters only. There is a 35 character limit to this field. The App name cannot be changed once the bot is created.|
Bot template |	Echo bot |Choose SDK v3. Select either C# or Node.js for this quickstart, then click Select.|
App service |plan/Location |	Your app service plan	Select an app service plan location. Your location choice can be any location listed, though it's often best to choose the same location as the bot service.|
Application Insights |	On |	Decide if you want to turn Application Insights On or Off. If you select On, you must also specify a regional location. Your location choice can be any location listed, though it's often best to choose the same location as the bot service.|
Microsoft App ID and password|	Auto create App ID and password	|Use this option if you need to manually enter a Microsoft App ID and password. Otherwise, a new Microsoft App ID and password will be created for you in the bot creation process.(Note : Please create Microsoft App ID and Microsoft App Password manually and notedown for future reference in other steps.)|



![Image3](https://user-images.githubusercontent.com/48123984/57965192-83679080-795e-11e9-8754-26f856989e60.jpg)
- Note : Please note down Microsoft App ID and Microsoft App Password for future reference.
- To create 'Microsoft Computer Vision' Key please follow the following steps to reach 'Computer Vision' Section in Microsoft Azure :
  - Click on 'Create a resource' in left side Menu .
  - Click on 'AI+Machine Learning' in ;Azure Marketplace' Menu of New Section.
  - Click on 'Computer Vision' in featured section for 'AI+Machine Learning'.
 
![Image2](https://user-images.githubusercontent.com/48123984/57964955-f7079e80-795a-11e9-9237-00a86c379998.jpg)

- Please provide all details in 'Create' section then click on 'Create' button.

![Image4](https://user-images.githubusercontent.com/48123984/57966105-5a013180-796b-11e9-9131-2ccf45a8c95e.jpg)

- Once deployment has been done search your cognitive services application through name which you have given while inserting information during create section of 
computer vision'.Once application have been open then go to "RESOURCE MANAGEMENT" section and select Keys.Copy both the keys and maintain for future reference.

![Image5](https://user-images.githubusercontent.com/48123984/57966664-bca9fb80-7972-11e9-8f67-15ca4d9f2266.jpg)

- Click on 'Overview' in your computer vision cognitive service application section then copy Endpoint from the right side section of web page.

![Image6](https://user-images.githubusercontent.com/48123984/57966876-8f128180-7975-11e9-94e8-21e9535f5159.jpg)

- Open your ImageAnalyticsBot Application in Microsoft visual studio 2017.Click on web.config in Solution Explorer section then paste here your 
Microsoft Application ID,Microsoft Application Password,Computer Vision Key,Computer vision End Point at the area specified as "Insert ------ " in the brackets <>.

![Image8](https://user-images.githubusercontent.com/48123984/57967435-94bf9580-797c-11e9-9889-dd60629033eb.jpg)
- Please Right Click on ImageAnalyticsBot in Solution Explorer Menu Section then select Publish.

![Image9](https://user-images.githubusercontent.com/48123984/57967532-fb917e80-797d-11e9-9189-4639619ad746.jpg)

- Publish Application as App service on same Azure account which you use to register application.

![Image10](https://user-images.githubusercontent.com/48123984/57967656-37791380-797f-11e9-9bbc-d71f23842c7c.jpg)

- Note down Site URL after Publishing Application as App Service for future reference.

![Image11](https://user-images.githubusercontent.com/48123984/57967709-daca2880-797f-11e9-98e0-5a0d7b22c69c.jpg)

- Login to your Azure Account and Search Web App Bot(Created in initial step).After openclick on the 'Settings' in 'BOT MANAGEMENT' section then 
paste Site URL collected from the visual studio and append api/messages (for example: https://xxxxxxxx-xxxxxxxxxx.azurewebsites.net/api/messages) in messaging endpoint input section in configuration section.Save all the details by clicking on Save button above to bot profile Heading.

![Image12](https://user-images.githubusercontent.com/48123984/57967898-c555fe00-7981-11e9-87bb-b1454d7f34da.jpg)

- click on 'Test in Web Chat' option in 'Bot Management' section to test functionality after deployment.

![Image13](https://user-images.githubusercontent.com/48123984/57968129-25e63a80-7984-11e9-8a61-36fc95cb7760.jpg).

- Click on 'Channels' in 'Bot Management' section and then select 'Edit' option in 'Connect to Channels' area.

![image14](https://user-images.githubusercontent.com/48123984/57968199-fb48b180-7984-11e9-908e-8b12d75584cd.jpg).

- Check and copy keys by click on 'Show' button below to 'Secret keys' section.
  - Copy iframe link provided in 'Embed code' section and paste keys in the URL area specified in 'src' variable .
  - User can use this iframe to implement webchat channel in any web page to communicate with chatbot.

![Image15](https://user-images.githubusercontent.com/48123984/57968322-3eefeb00-7986-11e9-971c-e095b2cb5943.jpg)






