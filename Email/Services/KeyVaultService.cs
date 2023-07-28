using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services
{
	public class KeyVaultService
	{
		public async Task<string> VaultDownloader(string key)
		{
			var kvUri = $"https://praktykinet2023.vault.azure.net/";
			var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

			var secret = client.GetSecret($"{key}").Value;
			return secret.Value;
		}
	}
}
