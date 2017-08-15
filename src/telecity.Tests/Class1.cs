using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace telecity.Tests
{
	public class Tests
	{
		[Test]
		public async Task Sandbox()
		{
			using (var httpClient = new HttpClient
			                        {
				                        DefaultRequestHeaders =
				                        {
					                        Authorization = new AuthenticationHeaderValue("Basic", File.ReadAllText(@"C:\elbatelecitybot.tc")),
					                        Accept = {new MediaTypeWithQualityHeaderValue("application/json")}
				                        }
			                        })
			{
				var response = await httpClient.GetStringAsync("http://elba-build-1/app/rest/buildQueue");
				var queue = JsonConvert.DeserializeObject<BuildQueueResponse>(response);
				Console.Out.WriteLine(queue.Count);
			}
		}
	}
}