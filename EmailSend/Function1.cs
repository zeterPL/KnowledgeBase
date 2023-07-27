using System;
using KnowledgeBase.Logic.Dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using SendGridHelper.Models;

namespace EmailSend
{
    public class Function1
    {

		[FunctionName("Function1")]
        public void Run([ServiceBusTrigger("email-queue", Connection = "connection")]Message messageFromServiceBus, ILogger log)
        {
			log.LogInformation($"C# ServiceBus queue trigger function processed message: {messageFromServiceBus}");

		}
    }
}
