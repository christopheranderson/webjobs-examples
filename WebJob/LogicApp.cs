using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.WebJobs.Extensions.WebHooks;
using System.Net.Http;
using System.Net;

namespace webjobslogicapps
{
    public class LogicApp
    {
        public static async Task Listener([WebHookTrigger] WebHookContext context, TraceWriter trace)
        {
            trace.Info("Route: /LogicApp/Listener received data");
            JObject dataEvent = JObject.Parse(await context.Request.Content.ReadAsStringAsync());

            trace.Info("Data: \n" + dataEvent.ToString());

            context.Response = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent((JObject.FromObject(new { 
                    message = "The bot says: " + dataEvent["message"]
                })).ToString())
            };
        }
    }
}
