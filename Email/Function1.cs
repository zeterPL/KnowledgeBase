using System;
using System.Text;
using Email.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Email
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([ServiceBusTrigger("email-queue", Connection = "connection")]string message, ILogger log)
        {

			log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");

            var messageBodyReceived = JsonConvert.DeserializeObject<MessageModel>(message);

            

		}
	}
}
