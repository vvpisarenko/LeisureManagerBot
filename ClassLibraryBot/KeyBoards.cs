using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ClassLibraryBot
{
    public class KeyBoards
    {

        ReplyKeyboardMarkup rkm = new ReplyKeyboardMarkup();
        public ReplyKeyboardMarkup ShowKeyBoard1()
        {
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
            return rkm;
        }
        public ReplyKeyboardMarkup ShowKeyBoard2()
        {
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
            return rkm;
        }
    }
}
