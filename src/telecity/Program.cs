using System;
using System.IO;
using Telegram.Bot;

namespace telecity
{
	internal static class Program
	{
		private static void Main()
		{
			var bot = new TelegramBotClient(File.ReadAllText(@"C:\elbatelecitybot.key"));
			bot.OnMessage += (o, args) => Console.WriteLine(args.Message.Text);
			bot.StartReceiving();
			Console.Out.WriteLine("Listening, press ENTER to quit...");
			Console.ReadLine();
		}
	}
}