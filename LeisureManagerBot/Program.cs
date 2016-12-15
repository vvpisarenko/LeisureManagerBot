using ClassLibraryBot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
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
                Console.WriteLine("Hello, my name is {0}", bot.Username);
            }
            catch { };
            var rkm = new ReplyKeyboardMarkup();
            var pero = new Repository();
            RootObject account = pero.Get();

            var offset = 0;


            int z, x, y = 0;

            string[] name = new string[100];
            string[] description = new string[100];
            string[] author = new string[100];

            string[] filmname = new string[101];
            string[] filmdescription = new string[101];
            string[] genre = new string[101];

            using (OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Книги.mdb;"))
            {

                OleDbCommand c = new OleDbCommand("SELECT * FROM Зарубежная", connection);

                connection.Open();
                OleDbDataReader r = c.ExecuteReader();



                name[0] = r.GetName(0) + "\t" + r.GetName(1) + "\t" + r.GetName(2);
                int i = 1;
                while (r.Read())
                {
                    name[i] = r[0].ToString();
                    description[i] = r[1].ToString();
                    author[i] = r[2].ToString();
                    i = i + 1;
                }

                z = i;

                OleDbCommand c1 = new OleDbCommand("SELECT * FROM Русская", connection);
                OleDbDataReader r1 = c1.ExecuteReader();
                while (r1.Read())
                {
                    name[i] = r1[0].ToString();
                    description[i] = r1[1].ToString();
                    author[i] = r1[2].ToString();
                    i = i + 1;
                }

                x = i;

                OleDbCommand c2 = new OleDbCommand("SELECT * FROM Фантастика", connection);
                OleDbDataReader r2 = c2.ExecuteReader();
                while (r2.Read())
                {

                    name[i] = r2[0].ToString();
                    description[i] = r2[1].ToString();
                    author[i] = r2[2].ToString();
                    i = i + 1;
                }

                y = i;

                connection.Close();
            }

            using (OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Фильмы.mdb;"))
            {

                OleDbCommand c = new OleDbCommand("SELECT * FROM Фильмы", connection);

                connection.Open();
                OleDbDataReader r = c.ExecuteReader();

                filmname[0] = r.GetName(0) + "\t" + r.GetName(1) + "\t" + r.GetName(2);
                int i = 1;
                while (r.Read())
                {
                    filmname[i] = r[0].ToString();
                    filmdescription[i] = r[1].ToString();
                    genre[i] = r[2].ToString();
                    i = i + 1;
                }

                connection.Close();
            }

            string[] eventname = new string[25];
            string[] categories = new string[25];
            string[] urls = new string[25];

            int k = 0;

            while (true)
            {
                var updates = await Bot.GetUpdatesAsync(offset);

                foreach (var update in updates)
                {
                    if (update.Message.Type == Types.Enums.MessageType.TextMessage && (update.Message.Text == "/start" || update.Message.Text == "\u27A1" + "Restart"))
                    {

                        await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                        await Task.Delay(2000);

                        rkm.OneTimeKeyboard = true;

                        rkm.Keyboard = new KeyboardButton[][]

                                {
                                        new KeyboardButton[]
                                        {
                                            new KeyboardButton("\uD83C\uDFA5" + "Films"),
                                            new KeyboardButton("\uD83D\uDCDA" + "Books")
                                        },

                                        new KeyboardButton[]
                                        {
                                           new KeyboardButton("\uD83C\uDFAD \uD83C\uDFA8 \uD83C\uDFA4 \uD83C\uDFC8"+"\n"+"Events from TimePad"),
                                           new KeyboardButton("\u27A1" + "Restart")

                                        }
                                };
                        await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Здравствуйте! \u263A Для продолжения выберите желаемый вариант проведения досуга, и бот предоставит вам его. \u2611 На ваш выбор - книги, фильмы или же различные мероприятия с TimePad. ", false, false, 0, rkm);

                        Console.WriteLine("Message: {0}", update.Message.Text);
                    }
                    else
                    {

                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && (update.Message.Text == "\uD83C\uDFAD \uD83C\uDFA8 \uD83C\uDFA4 \uD83C\uDFC8" + "\n" + "Events from TimePad" || update.Message.Text == "/events"))
                        {

                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                            await Task.Delay(2000);

                            rkm.OneTimeKeyboard = true;
                            rkm.Keyboard = new KeyboardButton[][]

                                {
                                        new KeyboardButton[]
                                        {
                                            new KeyboardButton("\uD83C\uDFA4"),
                                            new KeyboardButton( "\uD83C\uDFAD"),
                                            new KeyboardButton( "\uD83C\uDFC8")
                                        },

                                        new KeyboardButton[]
                                        {
                                            new KeyboardButton( "\uD83C\uDFA8"),
                                            new KeyboardButton( "Another"),
                                           new KeyboardButton("\u27A1" + "Restart")

                                        }
                                };
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/concerts" + " - \uD83C\uDFA4 Концерты" + "\n" + "\n" + "/theatres" + " - \uD83C\uDFAD Театры" + "\n" + "\n" + "/sport" + " - \uD83C\uDFC8 Спорт" + "\n" + "\n" + "/artsandculture" + " - \uD83C\uDFA8 Искусство и Культура" + "\n" + "\n" + "/another" + " - \u2728 Другое", false, false, 0, rkm);

                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }

                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && (update.Message.Text == "/concerts" || update.Message.Text == "\uD83C\uDFA4"))
                        {
                            for (int i = 0; i < 25; i++)
                            {
                                if (account.values[i].categories[0].name == "Концерты")
                                {
                                    eventname[k] = account.values[i].name;
                                    categories[k] = account.values[i].categories[0].name;
                                    urls[k] = account.values[i].url;
                                    k = k + 1;
                                }
                            }

                            if (k > 0)
                            {

                                Random concert = new Random();
                                int con = concert.Next(k - 1);

                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Категория: " + eventname[con] + "\n" + "\n" + "Вид: " + categories[con] + "\n" + "\n" + urls[con]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/concerts" + " - \uD83C\uDFA4 Концерты" + "\n" + "\n" + "/events - \uD83C\uDFAD \uD83C\uDFA8 \uD83C\uDFA4 \uD83C\uDFC8" + "\n" + "Choose another events from TimePad" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                                k = 0;
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            }

                            Console.WriteLine("Message: {0}", update.Message.Text);

                        }
                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text == "/theatres")
                        {                            
                            for (int i = 0; i < 25; i++)
                            {


                                if (account.values[i].categories[0].name == "Театры")
                                {
                                    eventname[k] = account.values[i].name;
                                    categories[k] = account.values[i].categories[0].name;
                                    urls[k] = account.values[i].url;
                                    k = k + 1;
                                }
                            }
                            if (k > 0)
                            {

                                Random theatre = new Random();
                                int the = theatre.Next(k - 1);

                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Категория: " + eventname[the] + "\n" + "\n" + "Вид: " + categories[the] + "\n" + "\n" + urls[the]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/theatres" + " - \uD83C\uDFAD Театры" + "\n" + "\n" + "/events - \uD83C\uDFAD \uD83C\uDFA8 \uD83C\uDFA4 \uD83C\uDFC8" + "\n" + "Choose another events from TimePad" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                                k = 0;
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            }

                            Console.WriteLine("Message: {0}", update.Message.Text);

                        }
                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && (update.Message.Text == "\uD83D\uDCDA" + "Books" || update.Message.Text == "/books"))
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);

                            await Task.Delay(2000);

                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/russian" + " - \uD83C\uDDF7 русская классика" + "\n" + "\n" + "/foreign" + " -  зарубежная классика" + "\n" + "\n" + "/fantastic" + " - \u2728 фантастика");

                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }

                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text == "/foreign")
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);

                            await Task.Delay(2000);
                            Random rand = new Random();
                            int num = rand.Next(z);


                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + name[num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Автор: " + author[num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + description[num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/foreign" + " - зарубежная классика" + "\n" + "\n" + "/books - \uD83D\uDCDA choose another books" + "\n" + "\n" + "/start - \u27A1 return to main menu");


                            Console.WriteLine("Message: {0}", update.Message.Text);

                        }
                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text == "/russian")
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);

                            await Task.Delay(2000);

                            Random rand = new Random();
                            int num = rand.Next(z, x);


                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + name[num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Автор: " + author[num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + description[num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/russian" + " - \uD83C\uDDF7 русская классика" + "\n" + "\n" + "/books - \uD83D\uDCDA choose another books" + "\n" + "\n" + "/start - \u27A1 return to main menu");

                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }
                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text == "/fantastic")
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);

                            await Task.Delay(2000);

                            Random rand = new Random();
                            int num = rand.Next(x, y);


                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + name[num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Автор: " + author[num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + description[num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/fantastic" + " - \u2728 фантастика" + "\n" + "\n" + "/books - \uD83D\uDCDA choose another books" + "\n" + "\n" + "/start - \u27A1 return to main menu");

                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }
                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && (update.Message.Text == "\uD83C\uDFA5" + "Films" || update.Message.Text == "/films"))
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);

                            await Task.Delay(2000);

                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/drama" + " - \uD83D\uDE22 Драма" + "\n" + "\n" + "/comedy" + " - \uD83D\uDE06 Комедия" + "\n" + "\n" + "/thriller" + " - \uD83D\uDCA3 Боевик" + "\n" + "\n" + "/anothergenre" + " - Другой жанр");

                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }
                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text == "/drama")
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);

                            await Task.Delay(2000);


                            string[] filmname1 = new string[101];
                            string[] genre1 = new string[101];
                            string[] filmdescription1 = new string[101];

                            int q = 0;

                            for (int i = 1; i < 101; i++)
                            {
                                if (genre[i].Contains("драма") == true || genre[i].Contains("Драма") == true)
                                {
                                    filmname1[q] = filmname[i];
                                    genre1[q] = genre[i];
                                    filmdescription1[q] = filmdescription[i];
                                    q = q + 1;
                                }

                            }

                            if (q > 0)
                            {
                                Random drama = new Random();
                                int d = drama.Next(q - 1);

                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + filmname1[d]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Жанр: " + genre1[d]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + filmdescription1[d]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/drama - \uD83D\uDE22 choose another drama" + "\n" + "\n" + "/films - \uD83C\uDFA5 choose another films" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            }

                            else
                            {
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            }

                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }
                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text == "/comedy")
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);

                            await Task.Delay(2000);
                            string[] filmname2 = new string[101];
                            string[] genre2 = new string[101];
                            string[] filmdescription2 = new string[101];

                            int q = 0;

                            for (int i = 1; i < 101; i++)
                            {
                                if (genre[i].Contains("Комедия") == true || genre[i].Contains("комедия") == true)
                                {
                                    filmname2[q] = filmname[i];
                                    genre2[q] = genre[i];
                                    filmdescription2[q] = filmdescription[i];
                                    q = q + 1;
                                }

                            }

                            if (q > 0)
                            {
                                Random comedy = new Random();
                                int c = comedy.Next(q - 1);

                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + filmname2[c]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Жанр: " + genre2[c]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + filmdescription2[c]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/comedy - \uD83D\uDE06 choose another comedy" + "\n" + "\n" + "/films - \uD83C\uDFA5 choose another films" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            }

                            else
                            {
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            }
                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }


                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text == "/thriller")
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);

                            await Task.Delay(2000);

                            string[] filmname3 = new string[101];
                            string[] genre3 = new string[101];
                            string[] filmdescription3 = new string[101];

                            int q = 0;

                            for (int i = 1; i < 101; i++)
                            {
                                if (genre[i].Contains("Боевик") == true || genre[i].Contains("боевик") == true || genre[i].Contains("триллер") == true || genre[i].Contains("Триллер") == true)
                                {
                                    filmname3[q] = filmname[i];
                                    genre3[q] = genre[i];
                                    filmdescription3[q] = filmdescription[i];
                                    q = q + 1;
                                }

                            }

                            if (q > 0)
                            {
                                Random thriller = new Random();
                                int t = thriller.Next(q - 1);

                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + filmname3[t]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Жанр: " + genre3[t]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + filmdescription3[t]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/thriller - \uD83D\uDCA3 choose another thriller" + "\n" + "\n" + "/films - \uD83C\uDFA5 choose another films" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            }

                            else
                            {
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            }

                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }


                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text == "/anothergenre")
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);

                            await Task.Delay(2000);

                            string[] filmname4 = new string[101];
                            string[] genre4 = new string[101];
                            string[] filmdescription4 = new string[101];

                            int q = 0;

                            for (int i = 1; i < 101; i++)
                            {
                                if (genre[i].Contains("Комедия") != true && genre[i].Contains("комедия") != true && genre[i].Contains("Боевик") != true && genre[i].Contains("боевик") != true
                                    && genre[i].Contains("драма") != true && genre[i].Contains("Драма") != true && genre[i].Contains("триллер") != true && genre[i].Contains("Триллер") != true)
                                {
                                    filmname4[q] = filmname[i];
                                    genre4[q] = genre[i];
                                    filmdescription4[q] = filmdescription[i];
                                    q = q + 1;
                                }

                            }

                            if (q > 0)
                            {
                                Random anothergenre = new Random();
                                int a = anothergenre.Next(q - 1);

                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + filmname4[a]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Жанр: " + genre4[a]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + filmdescription4[a]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/anothergenre -  choose another anothergenre" + "\n" + "\n" + "/films - \uD83C\uDFA5 choose another films" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            }

                            else
                            {
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            }

                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }
                    }
                   offset = update.Id + 1;
                }
                await Task.Delay(1000);
            }
        }
    }
}