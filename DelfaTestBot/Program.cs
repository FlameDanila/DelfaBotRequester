using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using TL;

namespace DelfaTestBot
{
    class Program
    {
        private static string token { get; set; } = "5183249647:AAHCx42xlNoIEZ51EXA2qo0lJe0e4mp_J4M";
        private static TelegramBotClient client;
        public static int Counter = 0;
        public static int Block = 0;
        public static string back = "Вернуться к прошлому вопросу";

        [Obsolete]
        static async Task Main(string[] args)
        {
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }

        [Obsolete]
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var ss = e.Message;
            var name = ss.From.FirstName + " " + ss.From.LastName;
            string ansver = "";

            try
            {
                while (true)
                {
                    if (ss.Text == "cтарт" || ss.Text == "пройти тест заново" || ss.Text == "start" || ss.Text == "/start" || ss.Text == back && Counter == 1)
                    {
                        Console.WriteLine("Пришло сообщение: " + ss.Text + " от пользователя " + name);
                        await client.SendTextMessageAsync(ss.Chat.Id, "Привет!👋\nУчебный центр Дельфа приветствует тебя, дорогой друг!\nЯ помогу тебе найти нужную информацию без звонка!", replyMarkup: StartButtons());
                        Counter = 0;
                        break;
                    }
                    if (ss.Text == "Обучение для взрослых👨" && Counter == 0 || ss.Text == back && Counter == 2 && Block == 1)
                    {
                        Console.WriteLine("Пришло сообщение: " + ss.Text + " от пользователя " + name);
                        await client.SendTextMessageAsync(ss.Chat.Id, "Учились ли Вы у нас ранее?", replyMarkup: Question4Buttons());
                        Counter = 1;
                        Block = 1;
                        break;
                    }
                    else if (ss.Text == "Да" && Counter == 1 && Block == 1 || ss.Text == back && Counter == 3 && Block == 1)
                    {
                        Console.WriteLine("Пришел ответ на вопрос: " + ss.Text + " от пользователя " + name);
                        await client.SendTextMessageAsync(ss.Chat.Id, "Пожалуйста, оцените качество преподавания по пяти-бальной шкале и получите скидку 50％ на последующие курсы!", replyMarkup: Question2Buttons());
                        Counter = 2;
                        break;
                    }
                    else if (ss.Text != "" && Counter == 2 && Block == 1 && ss.Text != back )
                    {
                        Console.WriteLine("Пришел ответ на вопрос: " + ss.Text + " от пользователя " + name);
                        await client.SendTextMessageAsync(ss.Chat.Id, "Можете написать, почему вы выбрали именно эту оценку?", replyMarkup: EmptyAnsver());
                        if (ss.Text == back) { }
                        else
                        {
                            ansver = ss.Text;
                        }
                        Counter = 3;
                        break;
                    }
                    else if (ss.Text != "" && Counter == 3 && Block == 1 || ss.Text == back && Counter == 4 && Block == 1)
                    {
                        Console.WriteLine("Пришел ответ на вопрос: " + ss.Text + " от пользователя " + name);
                        await client.SendTextMessageAsync(ss.Chat.Id, "Спасибо за ваш отзыв.\nВот перечень профессий, среди которых Вы точно найдёте что-то по вкусу😋", replyMarkup: Question3Buttons());
                        Counter = 4;
                        break;
                    }
                    if (ss.Text == "Обучение для детей👶" && Counter == 0 || ss.Text == back && Counter == 2 && Block == 2)
                    {
                        Console.WriteLine("Пришел ответ на вопрос: " + ss.Text + " от пользователя " + name);
                        await client.SendTextMessageAsync(ss.Chat.Id, "Возраст ребенка?", replyMarkup: Question6Buttons());
                        Counter = 1;
                        Block = 2;
                        break;
                    }
                    else if (ss.Text == "3-6 Лет" && Counter == 1 && Block == 2 || ss.Text == back && Counter == 3 && Block == 2)
                    {
                        Console.WriteLine("Пришел ответ на вопрос: " + ss.Text + " от пользователя " + name);
                        await client.SendTextMessageAsync(ss.Chat.Id, "Выберите направление", replyMarkup: EmptyAnsver());
                        Counter = 2;
                        break;
                    }
                    else if (ss.Text == "1-4 Класс" && Counter == 1 && Block == 2 || ss.Text == back && Counter == 3 && Block == 2)
                    {
                        Console.WriteLine("Пришел ответ на вопрос: " + ss.Text + " от пользователя " + name);
                        await client.SendTextMessageAsync(ss.Chat.Id, "Выберите направление", replyMarkup: EmptyAnsver());
                        Counter = 2;
                        break;
                    }
                    else if (ss.Text == "5-11 Класс" && Counter == 1 && Block == 2 || ss.Text == back && Counter == 3 && Block == 2)
                    {
                        Console.WriteLine("Пришел ответ на вопрос: " + ss.Text + " от пользователя " + name);
                        await client.SendTextMessageAsync(ss.Chat.Id, "Выберите направление", replyMarkup: EmptyAnsver());
                        Counter = 2;
                        break;
                    }
                    if (ss.Text == "О нас " && Counter == 0 || ss.Text == back && Counter == 2 && Block == 5)
                    {
                        Console.WriteLine("Пришел ответ на вопрос: " + ss.Text + " от пользователя " + name);
                        await client.SendTextMessageAsync(ss.Chat.Id, "Учебный центр «Дельфа» – это современное образовательное учреждение, специализирующееся в обучении в сфере IT-технологий 🖥.\n27 лет успешной работы на рынке образовательных услуг\n\nЛицензия на осуществление образовательной деятельности №026 от 23.04.2018", replyMarkup: Question6Buttons());
                        Counter = 1;
                        Block = 2;
                        break;
                    }
                    break;
                }
            }
            catch (Exception ex)
            { }

            //if (ss.Text.ToLower() == "да2" || (ss.Text.ToLower() == "вернуться к прошлому вопросу" && Counter == 4))
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + ss.Text);
            //    await client.SendTextMessageAsync(ss.Chat.Id, "Вопросов пройдено: 3");
            //    await client.SendTextMessageAsync(ss.Chat.Id, "Вопрос 4:\nНравится ли вам учить детей?", replyMarkup: Question3Buttons());
            //    Counter = 3;
            //}
            //if (ss.Text.ToLower() == "нравится" || (ss.Text.ToLower() == "вернуться к прошлому вопросу" && Counter == 5))
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + ss.Text);
            //    await client.SendTextMessageAsync(ss.Chat.Id, "Вопросов пройдено: 4");
            //    await client.SendTextMessageAsync(ss.Chat.Id, $"Успешно!\nВы прирождённый преподаватель!😉\nВероятней всего вам подойдёт профессия Младшего воспитателя🏫\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/obrazovaniie/mladshiy-vospitatel/"}", replyMarkup: EmptyAnsver());
            //    Counter = 4;
            //}
            //if (ss.Text.ToLower() == "не нравится" || (ss.Text.ToLower() == "вернуться к прошлому вопросу" && Counter == 5))
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + ss.Text);
            //    await client.SendTextMessageAsync(ss.Chat.Id, "Вопросов пройдено: 4");
            //    await client.SendTextMessageAsync(ss.Chat.Id, "Вопрос 5:\nХотите ли вы преображать людей?", replyMarkup: Question5Buttons());
            //    Counter = 4;
            //}
            //if (ss.Text.ToLower() == "хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + ss.Text);
            //    await client.SendTextMessageAsync(ss.Chat.Id, "Вопросов пройдено: 5");
            //    await client.SendTextMessageAsync(ss.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            //    Counter = 5;
            //}
            //if (ss.Text.ToLower() == "не хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + ss.Text);
            //    await client.SendTextMessageAsync(ss.Chat.Id, "Вопросов пройдено: 5");
            //    await client.SendTextMessageAsync(ss.Chat.Id, "Вопрос 6:\nЖелаете ли вы управлять персоналом, нанимать новые кадры?", replyMarkup: Question6Buttons());
            //    Counter = 5;
            //}
            //if (ss.Text.ToLower() == "желаю")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + ss.Text);
            //    await client.SendTextMessageAsync(ss.Chat.Id, "Вопросов пройдено: 6");
            //    await client.SendTextMessageAsync(ss.Chat.Id, $"Успешно!\nУ вас сильно выражены лидерские качества!😉\nВероятней всего вам подойдут профессии связанные с административным персоналом" +
            //        $"\nВыберите профессию по-душе:", replyMarkup: Question7Buttons());
            //}
            //if (ss.Text.ToLower() == "секретарь-администратор")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + ss.Text);
            //    await client.SendTextMessageAsync(ss.Chat.Id, $"Успешно!\nАлексей Алексеич, к вам посетитель! Спасибо{name}!😉\nВероятней всего вам подойдёт профессия Секретаря-администратора📝\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/professionalnye-otrasli/administrativnyy-personal/sekretar-administrator/"}", replyMarkup: EmptyAnsver());
            //}
            //if (ss.Text.ToLower() == "хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + ss.Text);
            //    await client.SendTextMessageAsync(ss.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            //}
            //if (ss.Text.ToLower() == "хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + ss.Text);
            //    await client.SendTextMessageAsync(ss.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            //}
            //if (ss.Text.ToLower() == "хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + ss.Text);
            //    await client.SendTextMessageAsync(ss.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            //}
            //if (ss.Text.ToLower() == "хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + ss.Text);
            //    await client.SendTextMessageAsync(ss.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            //}
            //if (ss.Text.ToLower() == "хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + ss.Text);
            //    await client.SendTextMessageAsync(ss.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            //}
        }
        static string Config(string what)
        {
            switch (what)
            {
                case "api_id": return "17257489";
                case "api_hash": return "1c8608e262882c49cb57d1640b46b559";
                case "phone_number": return "+79504951460";
                case "verification_code": Console.Write("Code: "); return Console.ReadLine();
                case "first_name": return "Danila";      // if sign-up is required
                case "last_name": return ".";        // if sign-up is required
                case "password": return "secret!";     // if user has enabled 2FA
                default: return null;                  // let WTelegramClient decide the default config
            }
        }
        private async Task SendMessage()
        {
            try
            {
                var wTLClient = new WTelegram.Client(Config);
                var my = await wTLClient.LoginUserIfNeeded();
                var resolved = await wTLClient.Contacts_ResolvePhone($"89199400273"); // username without the @
                await wTLClient.SendMessageAsync(resolved, "Пришел запрос на ");
                wTLClient.Dispose();
            }
            catch (Exception ex) { }
        }
        private static IReplyMarkup StartButtons()
        {
            //WebClient web = new WebClient();
            //Byte[] Data = web.DownloadData(""); //Загрузка страницы для вывода кнопок с сайта в бота

            //using (FileStream file = new FileStream(@"C:\Users\Public\Music\t.txt", FileMode.Create))
            //{
            //    Byte[] vs = Data;

            //    file.Write(vs, 0, vs.Length);
            //}

            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Обучение для взрослых👨" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Обучение для детей👶" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Летняя IT-школа☀" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Дополнительные услуги⚙" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "О нас❓" } }

                }
            };
        }
        private static IReplyMarkup Question2Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "★★★★★" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "★★★★☆" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "★★★☆☆" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "★★☆☆☆" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "★☆☆☆☆" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back } }
                }
            };
        }
        private static IReplyMarkup Question3Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "IT-Курсы" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Профобучение" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Профессии" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Информационная безопасность" }},
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back } }
                }
            };
        }
        private static IReplyMarkup Question4Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Да" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Нет" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back } }
                }
            };
        }
        private static IReplyMarkup EmptyAnsver()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back } }
                }
            };
        }
        private static IReplyMarkup Question5Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Хочу" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Не хочу" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back } }
                }
            };
        }
        private static IReplyMarkup Question6Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "3-6 Лет" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "1-4 Класс" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "5-11 Класс" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back } }
                }
            };
        }
    }
}