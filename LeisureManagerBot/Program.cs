using ClassLibraryBot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
        }
        static async Task Run()
        {
            var Bot = new TelegramBotClient("306835183:AAHQLsRrPzOeDfHhQvlOOYi0X5kRJke7ngg");
            try
            {
                var bot = await Bot.GetMeAsync();
                Console.WriteLine("Hello my name is {0}", bot.Username);
            }
            catch { };
            var rkm = new ReplyKeyboardMarkup();

            string city = "Москва";
            string url = "https://api.timepad.ru/v1/events.json?limit=25&skip=0&cities=" + city;
            string str;

            using (var webClient = new WebClient())
            {

                str = webClient.DownloadString(url);
            }

            byte[] bytes = Encoding.Default.GetBytes(str);
            str = Encoding.UTF8.GetString(bytes);

            RootObject account = JsonConvert.DeserializeObject<RootObject>(str);
        }

    }
}
