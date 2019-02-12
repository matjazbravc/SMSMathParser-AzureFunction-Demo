# SMS Math Parser - Azure Function Demo

Let's say you are a poor guy with an old handy without internet access and calculator. Any you badly need an math calculator. This sample can teach you how to build simple SMS mathematical expression parser which returns result as SMS back to sender. Anyway... :)

This demo explains how to send text messages (SMS) to [Twilio](https://www.twilio.com/) number, capture message with [Webhook](https://www.twilio.com/docs/glossary/what-is-a-webhook), parse mathematical expression and return result as text message by using [Twilio output bindings in Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-twilio).

This application demonstrate also the following functionalities:
- usage of Azure durable functions,
- [Twilio](https://www.twilio.com/) bindings in Azure Functions
- dependency injection with [Autofac](https://autofac.org/),
- logging with [Serilog](https://serilog.net/) sink to Azure Table storage,
- usage of [Mathos-Parser](https://github.com/MathosProject/Mathos-Parser),
- local testing with [ngrok](https://ngrok.com/)

## Prerequisites
- [Visual Studio](https://www.visualstudio.com/vs/community) 2017 15.5.5 or greater
- Azure Storage Emulator (the Storage Emulator is available as part of the [Microsoft Azure SDK](https://azure.microsoft.com/en-us/downloads/)),
- [Twilio](https://www.twilio.com) account,
- [ngrok](https://ngrok.com/) which allows you to expose a web server running on your local machine to the internet

To create and deploy functions, you also need:
- An active Azure subscription. If you don't have an Azure subscription, [free accounts](https://azure.microsoft.com/en-us/free/) are available.
- An Azure Storage account. To create a storage account, see [Create a storage account](https://docs.microsoft.com/en-us/azure/storage/common/storage-create-storage-account#create-a-storage-account).

## Recipe

**Step 1: Twilio**

Twilio is a cloud communications platform as a service company based in San Francisco, California. Twilio allows software developers to programmatically make and receive phone calls and send and receive text messages using its web service APIs.
You need free Twilio account and for that purpose you have to sign up for one [here](https://www.twilio.com/try-twilio). How to work with free Twilio trial account you can read [here](https://www.twilio.com/docs/usage/tutorials/how-to-use-your-free-trial-account).
![](https://github.com/matjazbravc/SMSMathParser-AzureFunction-Demo/blob/master/res/twilio_dashboard.jpg)

**Step 2: Update _local.settings.json_ file**
```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "AzureWebJobsDashboard": "UseDevelopmentStorage=true",
    "Logging.Storage.TableName": "SmsMathParserLog",
    "StorageAccount.ConnectionString": "UseDevelopmentStorage=true",
    "TwilioAccountSid": "<Put your Twilio Account SID here>",
    "TwilioAuthToken": "<Put your Twilio Auth Token here>",
    "TwilioPhoneNumber": "<Put your Twilio Phone Number here>",
  }
}
```
**Step 3: Start local function**

Build solution and run it with local Azure Functions runtime:
![](https://github.com/matjazbravc/SMSMathParser-AzureFunction-Demo/blob/master/res/function_local_runtime_1.jpg)

**Step 4: ngrok**

ngrok is a [Go](http://golang.org/) program, distributed as a single executable file for all major desktop platforms. This is super rad – no additional frameworks to install or other dependencies. [Grab the version](https://ngrok.com/download) for your development system of choice and simply unzip the file somewhere on your computer.
So how do we connect function to Twilio? Well, keep your project running. We are going to use ngrok to expose our local web server to the internet. Once you’ve got ngrok installed, open the command prompt or terminal and type ngrok.exe http -host-header=localhost 7071. You should get a forwarding URL that forwards traffic to your local code. Big ups to [Shreve](https://github.com/inconshreveable) for making such an amazing product.
![](https://github.com/matjazbravc/SMSMathParser-AzureFunction-Demo/blob/master/res/ngrok.jpg)

**Step 5: Configure Twilio to work with our local machine**

Then head over to the [Twilio console](https://www.twilio.com/console/phone-numbers/incoming), and update your Twilio phone number to use your ngrok URL when there are incoming messages.
![](https://github.com/matjazbravc/SMSMathParser-AzureFunction-Demo/blob/master/res/twilio_webhook.jpg)

**Step 6: Send SMS with mathematic expression**

Send SMS with expression **_(24*10)/8_**

![](https://github.com/matjazbravc/SMSMathParser-AzureFunction-Demo/blob/master/res/SMS_result.png)

End you will get **"Result: 30"** Happy? :)

## Licence

Licenced under [MIT](http://opensource.org/licenses/mit-license.php).
Contact me on [LinkedIn](https://si.linkedin.com/in/matjazbravc).
