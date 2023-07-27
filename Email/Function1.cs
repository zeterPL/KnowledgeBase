using System;
using System.Text;
using System.Threading.Tasks;
using Email.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;

namespace Email
{
    public class Function1
    {
        [FunctionName("Function1")]
        public async Task Run([ServiceBusTrigger("email-queue", Connection = "connection")]string message, ILogger log)
        {

			log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");

            var messageBodyReceived = JsonConvert.DeserializeObject<MessageModel>(message);

            Console.WriteLine(messageBodyReceived.PermissionsToString(messageBodyReceived.RequestedPermissions, messageBodyReceived.SenderName));
            
            var messageToSend = new SendGridMessage();

            messageToSend.AddTo(messageBodyReceived.ReceiverEmail);
			messageToSend.AddContent("text/html", messageBodyReceived.PermissionsToString(messageBodyReceived.RequestedPermissions, messageBodyReceived.SenderName));
            messageToSend.SetFrom(messageBodyReceived.SenderName);
            messageToSend.SetSubject("Permissions");
		}
	}
}
