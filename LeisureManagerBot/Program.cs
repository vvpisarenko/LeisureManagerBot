﻿using ClassLibraryBot;
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
            try
            {
                Run().Wait();
            }
            catch { };

        }

        static async Task Run()
        {
            var Bot = new TelegramBotClient("306835183:AAHQLsRrPzOeDfHhQvlOOYi0X5kRJke7ngg");
            try
            {
                var bot = await Bot.GetMeAsync();
                Console.Title = bot.Username;
                Console.WriteLine("Hello, my name is {0}", bot.Username);
            }
            catch { };

            WorkWithCategoriesClass w = new WorkWithCategoriesClass();
            KeyBoards kb = new KeyBoards();
            var repo = new Repository();
            Event account = repo.Get();

            var offset = 0;
            int z, x, y = 0;

            string[] name = new string[101];
            string[] description = new string[101];
            string[] author = new string[101];

            string[] filmname = new string[101];
            string[] filmdescription = new string[101];
            string[] genre = new string[101];


            var db1 = new DBClassReading();
            string[][] books = new string[3][];
            DelBook dbBooksdDelegate = new DelBook(db1.Book);
            books = dbBooksdDelegate(name, description, author, out z, out x, out y);

            string[][] films = new string[3][];
            DelFilm dbFilmsDelegate = new DelFilm(db1.Film);
            films = dbFilmsDelegate(filmname, filmdescription, genre);

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
                        kb.ShowKeyBoard1();
                        await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Здравствуйте! \u263A Для продолжения выберите желаемый вариант проведения досуга, и бот предоставит вам его. \u2611 На ваш выбор - книги, фильмы или же различные мероприятия с TimePad. ", false, false, 0, kb.ShowKeyBoard1());
                        Console.WriteLine("Message: {0}", update.Message.Text);
                    }
                    else
                    {
                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && (update.Message.Text == "\uD83C\uDFAD \uD83C\uDFA8 \uD83C\uDFA4 \uD83C\uDFC8" + "\n" + "Events from TimePad" || update.Message.Text == "/events"))
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                            await Task.Delay(2000);
                            kb.ShowKeyBoard2();
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/concerts" + " - \uD83C\uDFA4 Концерты" + "\n" + "\n" + "/theatres" + " - \uD83C\uDFAD Театры" + "\n" + "\n" + "/sport" + " - \uD83C\uDFC8 Спорт" + "\n" + "\n" + "/artsandculture" + " - \uD83C\uDFA8 Искусство и Культура" + "\n" + "\n" + "/another" + " - \u2728 Другое", false, false, 0, kb.ShowKeyBoard2());
                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }

                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && (update.Message.Text == "/concerts" || update.Message.Text == "\uD83C\uDFA4"))
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                            await Task.Delay(1500);
                            string str = "Концерты";
                            w.Circle(account, str, out eventname, out categories, out urls, out k);
                            if (k > 0)
                            {
                                Random concert = new Random();
                                int con = concert.Next(k - 1);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Категория: " + eventname[con] + "\n" + "\n" + "Вид: " + categories[con] + "\n" + "\n" + urls[con]);
                                kb.ShowKeyBoard2();
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/concerts" + " - \uD83C\uDFA4 Концерты" + "\n" + "\n" + "/events - \uD83C\uDFAD \uD83C\uDFA8 \uD83C\uDFA4 \uD83C\uDFC8" + "\n" + "Choose another events from TimePad" + "\n" + "\n" + "/start - \u27A1 return to main menu", false, false, 0, kb.ShowKeyBoard2());
                                k = 0;
                                Array.Clear(eventname, 0, 25);
                                Array.Clear(categories, 0, 25);
                                Array.Clear(urls, 0, 25);
                            }
                            else
                            {
                                await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                                await Task.Delay(1000);
                                kb.ShowKeyBoard2();
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu", false, false, 0, kb.ShowKeyBoard2());
                            }

                            Console.WriteLine("Message: {0}", update.Message.Text);

                        }
                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && (update.Message.Text == "/theatres" || update.Message.Text == "\uD83C\uDFAD"))
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                            await Task.Delay(1500);
                            string str = "Театры";
                            w.Circle(account, str, out eventname, out categories, out urls, out k);
                            if (k > 0)
                            {
                                Random theatre = new Random();
                                int the = theatre.Next(k - 1);

                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Категория: " + eventname[the] + "\n" + "\n" + "Вид: " + categories[the] + "\n" + "\n" + urls[the]);
                                kb.ShowKeyBoard2();
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/theatres" + " - \uD83C\uDFAD Театры" + "\n" + "\n" + "/events - \uD83C\uDFAD \uD83C\uDFA8 \uD83C\uDFA4 \uD83C\uDFC8" + "\n" + "Choose another events from TimePad" + "\n" + "\n" + "/start - \u27A1 return to main menu", false, false, 0, kb.ShowKeyBoard2());
                                k = 0;
                                Array.Clear(eventname, 0, 25);
                                Array.Clear(categories, 0, 25);
                                Array.Clear(urls, 0, 25);
                            }
                            else
                            {
                                await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                                await Task.Delay(1000);
                                kb.ShowKeyBoard2();
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu", false, false, 0, kb.ShowKeyBoard2());
                            }
                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }

                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && (update.Message.Text == "/sport" || update.Message.Text == "\uD83C\uDFC8"))
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                            await Task.Delay(1500);
                            string str = "Спорт";
                            w.Circle(account, str, out eventname, out categories, out urls, out k);
                            if (k > 0)
                            {
                                Random sport = new Random();
                                int spo = sport.Next(k - 1);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Категория: " + eventname[spo] + "\n" + "\n" + "Вид: " + categories[spo] + "\n" + "\n" + urls[spo]);
                                kb.ShowKeyBoard2();
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/sport" + " - \uD83C\uDFC8 Спорт" + "\n" + "\n" + "/events - \uD83C\uDFAD \uD83C\uDFA8 \uD83C\uDFA4 \uD83C\uDFC8" + "\n" + "Choose another events from TimePad" + "\n" + "\n" + "/start - \u27A1 return to main menu", false, false, 0, kb.ShowKeyBoard2());
                                k = 0;
                                Array.Clear(eventname, 0, 25);
                                Array.Clear(categories, 0, 25);
                                Array.Clear(urls, 0, 25);
                            }

                            else
                            {
                                await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                                await Task.Delay(1000);
                                kb.ShowKeyBoard2();
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu", false, false, 0, kb.ShowKeyBoard2());
                            }
                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }


                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && (update.Message.Text == "/artsandculture" || update.Message.Text == "\uD83C\uDFA8"))
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                            await Task.Delay(1500);
                            string str = "Искусство и культура";
                            w.Circle(account, str, out eventname, out categories, out urls, out k);
                            if (k > 0)
                            {
                                Random arts = new Random();
                                int a = arts.Next(k - 1);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Категория: " + eventname[a] + "\n" + "\n" + "Вид: " + categories[a] + "\n" + "\n" + urls[a]);
                                kb.ShowKeyBoard2();
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/artsandculture" + " - \uD83C\uDFA8 Искусство и Культура" + "\n" + "\n" + "/events - \uD83C\uDFAD \uD83C\uDFA8 \uD83C\uDFA4 \uD83C\uDFC8" + "\n" + "Choose another events from TimePad" + "\n" + "\n" + "/start - \u27A1 return to main menu", false, false, 0, kb.ShowKeyBoard2());
                                k = 0;
                                Array.Clear(eventname, 0, 25);
                                Array.Clear(categories, 0, 25);
                                Array.Clear(urls, 0, 25);
                            }

                            else
                            {
                                await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                                await Task.Delay(1000);
                                kb.ShowKeyBoard2();
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu", false, false, 0, kb.ShowKeyBoard2());
                            }
                            Console.WriteLine("Message: {0}", update.Message.Text);

                        }

                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && (update.Message.Text == "/another" || update.Message.Text == "Another"))
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                            await Task.Delay(1500);
                            for (int i = 0; i < 25; i++)
                            {
                                if (account.values[i].categories[0].name != "Искусство и культура" && account.values[i].categories[0].name != "Спорт"
                                    && account.values[i].categories[0].name != "Концерты" && account.values[i].categories[0].name != "Театры")
                                {
                                    eventname[k] = account.values[i].name;
                                    categories[k] = account.values[i].categories[0].name;
                                    urls[k] = account.values[i].url;
                                    k = k + 1;
                                }
                            }
                            if (k > 0)
                            {
                                Random another = new Random();
                                int ano = another.Next(k - 1);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Категория: " + eventname[ano] + "\n" + "\n" + "Вид: " + categories[ano] + "\n" + "\n" + urls[ano]);
                                kb.ShowKeyBoard2();
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/another" + " - \u2728 Другое" + "\n" + "\n" + "/events - \uD83C\uDFAD \uD83C\uDFA8 \uD83C\uDFA4 \uD83C\uDFC8" + "\n" + "Choose another events from TimePad" + "\n" + "\n" + "/start - \u27A1 return to main menu", false, false, 0, kb.ShowKeyBoard2());
                                k = 0;
                                Array.Clear(eventname, 0, 25);
                                Array.Clear(categories, 0, 25);
                                Array.Clear(urls, 0, 25);
                            }

                            else
                            {
                                await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                                await Task.Delay(1000);
                                kb.ShowKeyBoard2();
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu", false, false, 0, kb.ShowKeyBoard2());
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
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + books[0][num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Автор: " + books[2][num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + books[1][num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/foreign" + " - зарубежная классика" + "\n" + "\n" + "/books - \uD83D\uDCDA choose another books" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }
                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text == "/russian")
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                            await Task.Delay(2000);
                            Random rand = new Random();
                            int num = rand.Next(z, x);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + books[0][num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Автор: " + books[2][num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + books[1][num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/russian" + " - \uD83C\uDDF7 русская классика" + "\n" + "\n" + "/books - \uD83D\uDCDA choose another books" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }
                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text == "/fantastic")
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                            await Task.Delay(2000);
                            Random rand = new Random();
                            int num = rand.Next(x, y);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + books[0][num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Автор: " + books[2][num]);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + books[1][num]);
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

                        string[] filmname1 = new string[101];
                        string[] genre1 = new string[101];
                        string[] filmdescription1 = new string[101];
                        int q = 0;

                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text == "/drama")
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                            await Task.Delay(2000);
                            string string1 = "Драма";
                            string string2 = "драма";
                            w.CircleForFilms(string1, string2, genre, filmdescription, filmname, out filmname1, out filmdescription1, out genre1, out q);
                            if (q > 0)
                            {
                                Random drama = new Random();
                                int d = drama.Next(q - 1);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + filmname1[d]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Жанр: " + genre1[d]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + filmdescription1[d]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/drama - \uD83D\uDE22 choose another drama" + "\n" + "\n" + "/films - \uD83C\uDFA5 choose another films" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                                q = 0;
                                Array.Clear(filmname1, 0, 101);
                                Array.Clear(filmdescription1, 0, 101);
                                Array.Clear(genre1, 0, 101);
                            }

                            else
                            {
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                                await Task.Delay(1000);
                            }
                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }
                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text == "/comedy")
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                            await Task.Delay(2000);
                            string string1 = "Комедия";
                            string string2 = "комедия";
                            w.CircleForFilms(string1, string2, genre, filmdescription, filmname, out filmname1, out filmdescription1, out genre1, out q);
                            if (q > 0)
                            {
                                Random comedy = new Random();
                                int c = comedy.Next(q - 1);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + filmname1[c]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Жанр: " + genre1[c]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + filmdescription1[c]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/comedy - \uD83D\uDE06 choose another comedy" + "\n" + "\n" + "/films - \uD83C\uDFA5 choose another films" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                                q = 0;
                                Array.Clear(filmname1, 0, 101);
                                Array.Clear(filmdescription1, 0, 101);
                                Array.Clear(genre1, 0, 101);
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
                            string string1 = "боевик";
                            string string2 = "триллер";
                            w.CircleForFilms(string1, string2, genre, filmdescription, filmname, out filmname1, out filmdescription1, out genre1, out q);
                            if (q > 0)
                            {
                                Random thriller = new Random();
                                int t = thriller.Next(q - 1);

                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + filmname1[t]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Жанр: " + genre1[t]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + filmdescription1[t]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/thriller - \uD83D\uDCA3 choose another thriller" + "\n" + "\n" + "/films - \uD83C\uDFA5 choose another films" + "\n" + "\n" + "/start -\u27A1 return to main menu");
                                q = 0;
                                Array.Clear(filmname1, 0, 101);
                                Array.Clear(filmdescription1, 0, 101);
                                Array.Clear(genre1, 0, 101);
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
                            for (int i = 1; i < 101; i++)
                            {
                                if (genre[i].Contains("Комедия") != true && genre[i].Contains("комедия") != true && genre[i].Contains("Боевик") != true && genre[i].Contains("боевик") != true
                                && genre[i].Contains("драма") != true && genre[i].Contains("Драма") != true && genre[i].Contains("триллер") != true && genre[i].Contains("Триллер") != true)
                                {
                                    filmname1[q] = filmname[i];
                                    genre1[q] = genre[i];
                                    filmdescription1[q] = filmdescription[i];
                                    q = q + 1;
                                }
                            }

                            if (q > 0)
                            {
                                Random anothergenre = new Random();
                                int a = anothergenre.Next(q - 1);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Название: " + filmname1[a]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Жанр: " + genre1[a]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Описание: " + filmdescription1[a]);
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "/anothergenre - choose another anothergenre" + "\n" + "\n" + "/films - \uD83C\uDFA5 choose another films" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                                q = 0;
                                Array.Clear(filmname1, 0, 101);
                                Array.Clear(filmdescription1, 0, 101);
                                Array.Clear(genre1, 0, 101);
                            }

                            else
                            {
                                await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Ничего не найдено" + "\n" + "\n" + "/start - \u27A1 return to main menu");
                            }

                            Console.WriteLine("Message: {0}", update.Message.Text);
                        }

                        if (update.Message.Type == Types.Enums.MessageType.TextMessage && update.Message.Text != "/start" && update.Message.Text != "/events"
                            && update.Message.Text != "\uD83C\uDFAD \uD83C\uDFA8 \uD83C\uDFA4 \uD83C\uDFC8" + "\n" + "Events from TimePad"
                            && update.Message.Text != "/books" && update.Message.Text != "\uD83D\uDCDA" + "Books" && update.Message.Text != "\uD83C\uDFA5" + "Films"
                            && update.Message.Text != "/films" && update.Message.Text != "/concerts" && update.Message.Text != "\uD83C\uDFA4" && update.Message.Text != "\uD83C\uDFA8" && update.Message.Text != "\uD83C\uDFA4"
                            && update.Message.Text != "\uD83C\uDFC8" && update.Message.Text != " / theatres" && update.Message.Text != "/sport"
                            && update.Message.Text != "/artsandculture" && update.Message.Text != "/another" && update.Message.Text != "/thriller" && update.Message.Text != "/drama"
                            && update.Message.Text != "/comedy" && update.Message.Text != "/anothergenre" && update.Message.Text != "/russian" && update.Message.Text != "/foreign"
                            && update.Message.Text != "/fantastic")
                        {
                            await Bot.SendChatActionAsync(update.Message.Chat.Id, Types.Enums.ChatAction.Typing);
                            await Task.Delay(2000);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Поробуйте начать с команды \n \u27A1 /start!");
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