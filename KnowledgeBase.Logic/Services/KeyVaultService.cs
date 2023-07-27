using KnowledgeBase.Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace KnowledgeBase.Logic.Services
{
	public class KeyVaultService : IKeyVaultService
	{
		public async Task VaultDownloader(string key)
		{
			string secretName = key;
			var keyVaultName = "PraktykiNet2023";

			var kvUri = $"https://{keyVaultName}.vault.azure.net";

			var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

			Console.WriteLine($"Retrieving your secret from {keyVaultName}.");
			var secret = await client.GetSecretAsync(secretName);
			Console.WriteLine($"Your secret is '{secret.Value.Value}'.");

		}
	}
}
