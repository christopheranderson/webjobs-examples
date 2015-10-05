# WebJobs + Logic Apps SlackBot Example

This is a sample showing a more integrated WebJobs and Logic Apps scenario. Specifically, we'll show off how we can create a simple SlackBot using WebJobs and Logic Apps.

## What and Why

### What are WebJobs & Logic Apps?

WebJobs and the WebJobs SDK is a code first integration solution, which makes it easy to respond to a variety of events such as new Storage Blobs or a WebHook request. WebJobs can run in any Web, Mobile, or API Apps using Azure App Service.

Logic Apps are a configuration first integration solution, which makes it easy to build processes and workflows and integrates with various SaaS and enterprise applications. Logic Apps work together with your Web, Mobile, and API apps to deliver said integration.

### Why would I want to use WebJobs *and* Logic Apps?

Short and sweet, to take advantage of the best of both worlds. Logic Apps have a plethora of different API Apps that work out of the box and makes it easy to string them together. WebJobs are a wonderfully simple way of writing a little bit of code to get a lot of work done. By working together, we can get a great workflow experience while being free to write whatever code we want to do, using the existing API Apps along the way.

## Set up

### Dependencies

* VS 2013/2015 w/ Azure SDK installed
* An Azure Account
* Slack (you'll need to be able to add an integration)

### Deployment

0. Create a Web App and a Logic App.

1. Clone the project

    `git clone https://github.com/christopheranderson/webjobs-logicapps.git`

2. Deploy the WebJob

   0. Open the VS solution - [`WebJobs-LogicApps.sln`](./WebJobs-LogicApps.sln)
   1. Right-click on the WebJob Project
   2. Select `Publish as WebJob`.
   3. Select your Web App you just created and click publish.
   4. You'll also want to identify your site credentials.
     * You can do this by downloading your publish profile from the Azure portal and opening the file in a text editor. Look for a `$` to find your username and password.

3. Configure your Logic App

   0. Open your Logic App in the Azure portal
   1. Set up the HTTP Listener API App & trigger
       0. In the right menu, find the HTTP Listener in the Marketplace and click on it
       1. Keep the default settings and click create
       2. Wait for it to finish and when it says 'Double Click to configure', click on the arrow next to said text to continue set up.
       3. Choose POST and click the green checkmark
   2. Set up the HTTP request
       0. Enter the URL of your WebJob
           * It should be `https://{{sitename}}.scm.azurewebsites.net/api/continuouswebjobs/webjobslogicapps/passthrough/LogicApp/Listener`
           * You can test with something like PostMan. You'll need to use Basic Auth with your site credentials. It should be a `POST` request and the body should be a raw `application/json` like so:
               ```
               {
                 "message":"{{Whatever you want right here}}"
          }
               ```
       1. Set up Basic Auth in the Authentication input
           * This needs to be a serialized JSON object like so:
           ```
           {"username":"{{user}}","password":"{{pass}}","type":"basic"}
           ```
       2. The Body should be the Content of the HTTP Listener

   3. Set up the Slack Connector
      0. In the right menu, find the Slack connector in the Marketplace and click on it
      1. Wait for it to create
      2. Click Authorize, sign into your Slack, and accept
      3. Choose the 'Post Message' option
      4. The message is included in the body of the WebJob response
          * This can be tricky, because you need to parse the response before accessing it. Take a look at this sample: `@{parse(body('http')).message}`
      5. The channel name is actually the channel id.
          * You can use the [Slack API site](https://api.slack.com/methods/channels.list/test) to learn your channel id. Just click `Test Method` and do a `ctrl+f` search for your channel name.

  4. Hit save and you're good to go.

4. To start up your Logic App, send it a post message.
  0. Find your HTTP Listener API App and locate the URL. If you set up Basic Auth on your API App, note the username/password.
  1. With PostMan, enter the URL. If there is Basic Auth, enter that info.
  2. Set the Body to be a raw JSON object like so:
    ```
    {
      "message":"{{Whatever you want right here}}"
  }
    ```
  3. Hit send and you should get a response id back.
  4. If all goes well, you'll see a message in your Slack channel. If it doesn't, look in your Logic App logs to see what went wrong.

## Wrap up

This should help you figure out how to get this working for your scenario. You can use Logic Apps to string together your various WebJobs or to do some more intense analysis of Tweets; the world is your oyster! While these scenarios work, there is still room to make them better, so feel free to leave us feedback on [Twitter](https://twitter.com/logicappsio). (If you don't like Twitter, just create an issue). We'll be working hard to find ways to make it even easier to integrate these powerful tools together.

## License

See [./LICENSE] file
