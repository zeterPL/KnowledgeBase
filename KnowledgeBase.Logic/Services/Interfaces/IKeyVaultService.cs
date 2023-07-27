using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services.Interfaces
{
	public interface IKeyVaultService
	{
		public  Task VaultDownloader(string key);
	}
}
