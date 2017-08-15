using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace telecity
{
	internal static class Program
	{
		private static TelegramBotClient bot;

		private static void Main()
		{
			bot = new TelegramBotClient(File.ReadAllText(@"C:\elbatelecitybot.key"));
			bot.OnMessage += OnMessage;
			bot.StartReceiving();
			Console.Out.WriteLine("Listening, press ENTER to quit...");
			Console.ReadLine();
		}

		private static async void OnMessage(object o, MessageEventArgs args)
		{
			try
			{
				var messageText = args.Message.Text;
				Console.WriteLine(messageText);

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
					await bot.SendTextMessageAsync(args.Message.Chat.Id, $"Queued builds: *{queue.Count}*", ParseMode.Markdown);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}