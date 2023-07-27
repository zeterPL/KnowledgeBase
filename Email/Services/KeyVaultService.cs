using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

namespace KnowledgeBase.Logic.Services
{
	public class KeyVaultService
	{
		public async Task<KeyVaultSecret> VaultDownloader(string key)
		{
			string secretName = key;
			var keyVaultName = "PraktykiNet2023";

			var kvUri = $"https://{keyVaultName}.vault.azure.net";

			var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

			Console.WriteLine($"Retrieving your secret from {keyVaultName}.");
			var secret = await client.GetSecretAsync(secretName);
			Console.WriteLine($"Your secret is '{secret.Value.Value}'.");

			return secret;
		}
	}
}
