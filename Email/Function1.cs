using Email.Models;
using KnowledgeBase.Logic.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
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

			var vault = new KeyVaultService();
			messageReceived.SenderName = messageReceived.SenderName.Replace(" ", "");
			var mailSender = vault.VaultDownloader(messageReceived.SenderName+"Mail").Result;

			var senderPassword = new KeyVaultService();
			var senderMailPassword = senderPassword.VaultDownloader(messageReceived.SenderName+"Password").Result;

			MailMessage mailMessage = new MailMessage();
			mailMessage.From = new MailAddress(mailSender);
			mailMessage.To.Add(messageReceived.ReceiverEmail);
			mailMessage.Subject = "Permissions";
			mailMessage.Body = messageReceived.PermissionsToString(messageReceived.RequestedPermissions, messageReceived.SenderName);

			var smtpHost = new KeyVaultService();
			var hostToSend = smtpHost.VaultDownloader("gmail").Result;

			SmtpClient smtpClient = new SmtpClient();
			smtpClient.Host = hostToSend;
			smtpClient.Port = 587;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new NetworkCredential(mailSender, senderMailPassword);
			smtpClient.EnableSsl = true;
			
			smtpClient.Send(mailMessage);
		}
	}
}
