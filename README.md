# Language Understanding with the Vonage SMS API

This repository contains a sample leveraging the Vonage SMS API and Microsoft's Language Understanding ([Luis](https://luis.ai)) platform.

## How to Run this sample

### Prerequisites

- You'll need a Nexmo Account - if you don't have one you can sign up [here](https://dashboard.nexmo.com/sign-up)
- Visual Studio or Rider - Visual Studio 2019 was used to create this
- The .NET Core 3.1 runtime
- A Luis Ai Account - you can sign up [here](https://www.luis.ai/)
- Optional: [ngrok](https://ngrok.com/) to test

### Configuring Luis

You can play with Luis yourself in a manner consistent with the blog post - or you can simply upload [DeliverySample.json](DeliverySample.json) from this repo as your Luis model

### Add Environment Variables

Open in Visual Studio, right click on the project and go to properties. In the Debug tab scroll down to enviornment variables and add the following

Variable | Description
----|------------
NEXMO_API_KEY | Your Nexmo Api Key from the [dashboard](https://dashboard.nexmo.com/settings)
NEXMO_API_Secret | Your Nexmo Api Secret from the [dashboard](https://dashboard.nexmo.com/settings)
LUIS_PREDICTION_KEY | This is the key from Luis
LUIS_ENDPOINT_NAME | The endpoint url from luis e.g. https://westus.api.cognitive.microsoft.com
LUIS_APP_ID | the Guid App ID from Luis

Alternatively you can access the settings directly in [launchSettings.json](LuisVonageDemo/Properties/launchSettings.json)

### Setting up ngrok

Check what port your assigning for IIS express and configure ngrok with the following command:

```sh
ngrok http --host-header="localhost:PORT_NUMBER" http://localhost:PORT_NUMBER
```

replacing PORT_NUMBER with the port number for IIS

### Configuring webhooks

Now all that's left to do is to go to the settings page in the [dashboard](https://dashboard.nexmo.com/settings) and just change the inbound messages url to `http://UNIQUE_NGROK_ENDPOINT.ngrok.io/webhooks/inbound` replacing `UNIQUE_NGROK_ENDPOINT` with the random set of charecters produced by ngrok. Thus in the example above the endpoint would be `http://dc0feb1d.ngrok.io/webhooks/inbound` that will point nexmo at your IIS express server and allow us to receive messages.

## Time to Test

Now just fire up IIS Express and start texting your Nexmo number. You can test with a phrase like `Send chicken wings to 7287 North Cottage Ave. Camden, NJ 08105 from Popeye's`
