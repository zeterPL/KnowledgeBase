using Email.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mail;

namespace Email
{
	public class Function1
	{
		[FunctionName("Function1")]
		public void Run([ServiceBusTrigger("email-queue", Connection = "connection")] string message, ILogger log)
		{

			log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");

			var messageReceived = JsonConvert.DeserializeObject<MessageModel>(message);

			MailMessage mailMessage = new MailMessage();
			mailMessage.From = new MailAddress("");
			mailMessage.To.Add("");
			mailMessage.Subject = "Permissions";
			mailMessage.Body = messageReceived.PermissionsToString(messageReceived.RequestedPermissions, messageReceived.SenderName);

			SmtpClient smtpClient = new SmtpClient();
			smtpClient.Host = "smtp.gmail.com";
			smtpClient.Port = 587;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new NetworkCredential("", "");
			smtpClient.EnableSsl = true;

			smtpClient.Send(mailMessage);
		}
	}
}
