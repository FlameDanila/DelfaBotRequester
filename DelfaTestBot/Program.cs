using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using System.IO;
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
        public static string back = "⬅️Назад";
        public static string requestProfession = "";
        public static string requestName = "";
        public static string requestPhone = "";

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
            var username = ss.From.Id;
            Console.WriteLine(username.ToString());
            string ansver = "";

            var user = await Select($"select * from usersRequest where id like '{username}'");
            if(user.Rows.Count == 0)
            {
                await Select($"INSERT INTO usersRequest (id, block, counter) values('{username}', '0', '0')");
            }
            user = await Select($"select * from usersRequest where id like '{username}'");

            int Counter = 0;
            int Block = 0;

            for(int i = 0; i < user.Rows.Count; i++)
            {
                Counter = Convert.ToInt32(user.Rows[i][1]);
                Block = Convert.ToInt32(user.Rows[i][0]);
            }

            Console.WriteLine("Коунтер " + Counter + "блок " + Block);

            try
            {
                while (true)
                {
                    if (ss.Text == "Начать заново" || ss.Text == "start" || ss.Text == "/start" || ss.Text == back && Counter == 1)
                    {
                        await client.SendTextMessageAsync(ss.Chat.Id, "Привет!👋\nУчебный центр Дельфа приветствует тебя, дорогой друг!\nЯ помогу тебе найти нужную информацию без звонка!", replyMarkup: StartButtons());
                        Select($"update usersRequest set counter = 0 where id = '{username}'");
                        Select($"update usersRequest set block = 0 where id = '{username}'");
                        break;
                    }
                    if (ss.Text == "Обучение для взрослых👨" && Counter == 0 || ss.Text == back && Counter == 2 && Block == 1)
                    {
                        await client.SendTextMessageAsync(ss.Chat.Id, "Учились ли Вы у нас ранее?", replyMarkup: Question4Buttons());
                        Select($"update usersRequest set counter = 1 where id = '{username}'");
                        Select($"update usersRequest set block = replace(block,0,1) where id = '{username}'");
                        break;
                    }
                    if (ss.Text == "Да" && Counter == 1 && Block == 1 || ss.Text == back && Counter == 3 && Block == 1)
                    {

                        await client.SendTextMessageAsync(ss.Chat.Id, "Пожалуйста, оцените качество преподавания по пяти-бальной шкале и получите скидку 50％ на последующие курсы!", replyMarkup: Question2Buttons());
                        Select($"update usersRequest set counter = 2 where id = '{username}'");
                        break;
                    }
                    if (ss.Text != "" && Counter == 2 && Block == 1 && ss.Text != back  || ss.Text == back && Counter == 4 && Block == 1)
                    {
                        await client.SendTextMessageAsync(ss.Chat.Id, "Можете написать, почему вы выбрали именно эту оценку?", replyMarkup: EmptyAnsver2());
                        if (ss.Text == back || ss.Text == "Начать заново") { }
                        else
                        {
                            Select($"update usersRequest set score = '{ss.Text}' where id = '{username}'");
                        }
                        Select($"update usersRequest set counter = 3 where id = '{username}'");
                        break;
                    }
                    if (ss.Text != "" && Counter == 3 && Block == 1 || ss.Text == back && Counter == 4 && Block == 1)
                    {
                        Select($"update usersRequest set ansver = '{ss.Text}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "Спасибо за ваш отзыв.\nВот перечень профессий, среди которых Вы точно найдёте что-то по вкусу😋", replyMarkup: Question3Buttons());
                        Select($"update usersRequest set counter = 4 where id = '{username}'");
                        break;
                    }
                    if (ss.Text == "Обучение для детей👶" && Counter == 0 || ss.Text == back && Counter == 2 && Block == 2)
                    {

                        await client.SendTextMessageAsync(ss.Chat.Id, "Возраст ребенка?", replyMarkup: Question6Buttons());
                        Select($"update usersRequest set counter = 1 where id = '{username}'");
                        Select($"update usersRequest set block = 2 where id = '{username}'");
                        break;
                    }
                    if (ss.Text == "3-6 Лет" && Counter == 1 && Block == 2 || ss.Text == back && Counter == 3 && Block == 2)
                    {

                        await client.SendTextMessageAsync(ss.Chat.Id, "Выберите направление", replyMarkup: EmptyAnsver());
                        Select($"update usersRequest set counter = 2 where id = '{username}'");

                        break;
                    }
                    if (ss.Text == "1-4 Класс" && Counter == 1 && Block == 2 || ss.Text == back && Counter == 3 && Block == 2)
                    {

                        await client.SendTextMessageAsync(ss.Chat.Id, "Выберите направление", replyMarkup: EmptyAnsver());
                        Select($"update usersRequest set counter = 2 where id = '{username}'");

                        break;
                    }
                    if (ss.Text == "5-11 Класс" && Counter == 1 && Block == 2 || ss.Text == back && Counter == 3 && Block == 2)
                    {

                        await client.SendTextMessageAsync(ss.Chat.Id, "Выберите направление", replyMarkup: EmptyAnsver());
                        Select($"update usersRequest set counter = 2 where id = '{username}'");

                        break;
                    }
                    if (ss.Text == "Летняя IT-школа☀" && Counter == 1 && Block == 2 || ss.Text == "Летняя IT-школа☀" && Counter == 0 || ss.Text == back && Counter == 2 && Block == 3 || ss.Text == back && Counter == 7 && Block == 3)
                    {
                        await client.SendTextMessageAsync(ss.Chat.Id, "Проведи активное лето с учебным центром Дельфа.\nСамые интересные и актуальные направления только у нас!", replyMarkup: Question7Buttons());
                        Select($"update usersRequest set counter = 1 where id = '{username}'");;
                        Select($"update usersRequest set block = 3 where id = '{username}'");
                        break;
                    }
                    if (ss.Text == "Направления" && Counter == 1 && Block == 3 || ss.Text == back && Counter == 3 && Block == 3)
                    {
                        await client.SendTextMessageAsync(ss.Chat.Id, "Выберите направление: ", replyMarkup: Question8Buttons());
                        Select($"update usersRequest set counter = 2 where id = '{username}'");;
                        break;
                    }
                    if (ss.Text == "Расписание смен📆" && Counter == 1 && Block == 3)
                    {
                        await client.SendTextMessageAsync(ss.Chat.Id, "Июль\n3 поток\n04.07 - 15.07\n4 поток\n18.07 - 29.07\n\nАвгуст\n5 поток\n01.08 - 12.08\n6 поток\n15.08 - 26.08", replyMarkup: EmptyAnsver());
                        Select($"update usersRequest set counter = 7 where id = '{username}'"); ;
                        break;
                    }
                    if (ss.Text == "Системное администрирование 12+" && Counter == 2 && Block == 3 || ss.Text == back && Counter == 4 && Block == 3)
                    {
                        Select($"update usersRequest set selectedProfession = '{ss.Text}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "На занятиях школы «Системного администрирования» ребята разберутся с тем, как устроен компьютер, а также научатся проводить техническое обслуживание и ремонт компьютерной техники.\n\n(12 - 16 лет)\n\nПолный день\nc 9:00 до 17:00\n19 000 руб\n\nНеполный день\nс 11:00 до 15:00\n13 000 руб - смена(10 дней)", replyMarkup: request1Stage());
                        Select($"update usersRequest set counter = 3 where id = '{username}'");;
 
                        break;
                    }
                    if (ss.Text == "Play studio 8+" && Counter == 2 && Block == 3 || ss.Text == back && Counter == 4 && Block == 3)
                    {
                        Select($"update usersRequest set selectedProfession = '{ss.Text}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "«Play Studio» - Комплекс занятий направленных на выявление скрытого творческого потенциала ребёнка.\nДети попробуют себя в самых разных сферах, в том числе:\n-робототехнике;\n-химии;\n-авиа-моделировании;\n-режиссуре.\n\n(8-10 лет)\n\nПолный день\nc 9:00 до 17:00\n19 000 руб - смена (10 дней)\n\nНеполный день\nс 09:00 до 13:00\n13 000 руб - смена (10 дней)", replyMarkup: request1Stage());
                        Select($"update usersRequest set counter = 3 where id = '{username}'");;
 
                        break;
                    }
                    if (ss.Text == "Школа видеоблогера 11+" && Counter == 2 && Block == 3 || ss.Text == back && Counter == 4 && Block == 3)
                    {
                        Select($"update usersRequest set selectedProfession = '{ss.Text}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "На занятиях «Школы видеоблогера» ребенок не только активно задействует свой творческий потенциал, но и научится основам видеосъемки и монтажа и создаст свои видеоролики для социальных сетей\n\n(11 - 14 лет)\n\nПолный день\nc 9:00 до 17:00\n19 000 руб - смена(10 дней)\n\nНеполный день\nс 11:00 до 15:00\n13 000 руб - смена(10 дней)", replyMarkup: request1Stage());
                        Select($"update usersRequest set counter = 3 where id = '{username}'");;
 
                        break;
                    }
                    if (ss.Text == "Школа GameDev 11+" && Counter == 2 && Block == 3 || ss.Text == back && Counter == 4 && Block == 3)
                    {
                        Select($"update usersRequest set selectedProfession = '{ss.Text}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "В летней IT- школе «Разработка игр» ребят ждут нереально крутые занятия – погружение в виртуальный мир программирования и 3D-моделирования.\n\nНа уроках гейм - дизайна дети проявят свою фантазию, создавая миры и собственную Вселенную, а также продумают и реализуют игру для Android.\n\n(11 - 14 лет)\n\nПолный день\nc 9:00 до 17:00\n19 000 руб - смена(10 дней)\n\nНеполный день\nс 11:00 до 15:00\n13 000 руб - смена(10 дней)", replyMarkup: request1Stage());
                        Select($"update usersRequest set counter = 3 where id = '{username}'");;
 
                        break;
                    }
                    if (ss.Text == "Мобильная фотография 11+" && Counter == 2 && Block == 3 || ss.Text == back && Counter == 4 && Block == 3)
                    {
                        Select($"update usersRequest set selectedProfession = '{ss.Text}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "Курс предназначен для фотолюбителей, которые стремятся получать отличные фотографии с помощью современных смартфонов.\n\nНа курсе ребята детально изучат все необходимые правила, чтобы создавать свои собственные шедевры.\n\n(11 - 14 лет)\n\nПолный день\nc 9:00 до 17:00\n19 000 руб - смена(10 дней)\n\nНеполный день\nс 11:00 до 15:00\n13 000 руб - смена(10 дней)", replyMarkup: request1Stage());
                        Select($"update usersRequest set counter = 3 where id = '{username}'");;
 
                        break;
                    }
                    if (ss.Text == "Школа инжиниринга умных вещей 12+" && Counter == 2 && Block == 3 || ss.Text == back && Counter == 4 && Block == 3)
                    {
                        Select($"update usersRequest set selectedProfession = '{ss.Text}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "На смене «Школы инжиниринга умных вещей» ребята поработают над изобретательскими проектами по созданию устройств и систем для организации «умного дома», научатся 3D-моделированию и программированию микроконтроллеров на C++.\n\n(12 - 16 лет)\n\nПолный день\nc 9:00 до 17:00\n19 000 руб - смена(10 дней)\n\nНеполный день\nс 11:00 до 15:00\n13 000 руб - смена(10 дней) ", replyMarkup: request1Stage());
                        Select($"update usersRequest set counter = 3 where id = '{username}'");;
 
                        break;
                    }
                    if (ss.Text == "Оставить заявку на занятие" && Counter == 3 && Block == 3 || ss.Text == back && Counter == 5 && Block == 3)
                    {

                        await client.SendTextMessageAsync(ss.Chat.Id, "Введите номер телефона, чтобы мы могли связаться с Вами.", replyMarkup: EmptyAnsver());
                        Select($"update usersRequest set counter = 4 where id = '{username}'");;
                        break;
                    }
                    if (ss.Text != "" && Counter == 4 && Block == 3)
                    {
                        Select($"update usersRequest set phoneNumber = '{ss.Text}' where id = '{username}'");
                        Select($"update usersRequest set name = '{name}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "Спасибо за заявку, мы позвоним Вам в ближайшее время, чтобы обсудить условия обучения.", replyMarkup: EmptyAnsver());
                        await SendMessage(username.ToString());
                        Select($"update usersRequest set counter = 5 where id = '{username}'");;
 
                        break;
                    }
                    if (ss.Text == "О нас❓" && Counter == 0 || ss.Text == back && Counter == 2 && Block == 5)
                    {

                        await client.SendTextMessageAsync(ss.Chat.Id, "Учебный центр «Дельфа» – это современное образовательное учреждение, специализирующееся в обучении в сфере IT-технологий 🖥.\n27 лет успешной работы на рынке образовательных услуг\n\nЛицензия на осуществление образовательной деятельности №026 от 23.04.2018", replyMarkup: Question9Buttons());
                        Select($"update usersRequest set counter = 1 where id = '{username}'");;
                        Select($"update usersRequest set block = 5 where id = '{username}'");
                        break;
                    }
                    if (ss.Text == "Контактные данные📕" && Counter == 1 && Block == 5)
                    {

                        await client.SendTextMessageAsync(ss.Chat.Id, "https://delfa72.ru\n\nул. Республики, 61\n\n+7 (3452) 38-77-77\n+ 7(800) 250 - 11 - 10\n\ninfo@delfa72.ru", replyMarkup: EmptyAnsver());
                        Select($"update usersRequest set counter = 2 where id = '{username}'");;

                        break;
                    }
                    if (ss.Text == "График работы⏰" && Counter == 1 && Block == 5)
                    {

                        await client.SendTextMessageAsync(ss.Chat.Id, "Понедельник - Пятница: 08:00 - 20:00\nСуббота: 09:00 - 19:00\nВоскресенье: 09:00 - 18:00", replyMarkup: EmptyAnsver());
                        Select($"update usersRequest set counter = 2 where id = '{username}'");;

                        break;
                    }
                    if (ss.Text == "Фотогалерея📷" && Counter == 1 && Block == 5)
                    {

                        await client.SendTextMessageAsync(ss.Chat.Id, "Посмотреть фотографии вы можете на сайте:\n https://delfa72.ru/galereya/", replyMarkup: EmptyAnsver());
                        Select($"update usersRequest set counter = 2 where id = '{username}'");;

                        break;
                    }
                    if (ss.Text == "Дополнительные услуги⚙" && Counter == 0 || ss.Text == back && Counter == 2 && Block == 4 || ss.Text == back && Counter == 8 && Block == 4 || ss.Text == back && Counter == 5 && Block == 4)
                    {

                        await client.SendTextMessageAsync(ss.Chat.Id, "Выберите услугу: ", replyMarkup: Question10Buttons());
                        Select($"update usersRequest set counter = 1 where id = '{username}'");;
                        Select($"update usersRequest set block = 4 where id = '{username}'");
                        break;
                    }
                    if (ss.Text == "Ремонт компьютеров🔧" && Counter == 1 && Block == 4 || ss.Text == back && Counter == 3 && Block == 4)
                    {
                        Select($"update usersRequest set selectedProfession = '{ss.Text}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "Если Вы являетесь нашим клиентом, то у Вас есть возможность принести свой компьютер на диагностику к дипломированному специалисту.🎓\n\nПосле тщательной проверки и в случае выявления проблем мы осуществим мелкий ремонт.🛠\nБЕСПЛАТНО.", replyMarkup: request2Stage());
                        Select($"update usersRequest set counter = 2 where id = '{username}'");;

                        break;
                    }
                    if (ss.Text == "Оставить заявку" && Counter == 2 && Block == 4 || ss.Text == back && Counter == 4 && Block == 4)
                    {
                        await client.SendTextMessageAsync(ss.Chat.Id, "Введите номер телефона, чтобы мы могли связаться с Вами.", replyMarkup: EmptyAnsver());
                        Select($"update usersRequest set counter = 3 where id = '{username}'");;
                        break;
                    }
                    if (ss.Text != "" && Counter == 3 && Block == 4)
                    {
                        Select($"update usersRequest set phoneNumber = '{ss.Text}' where id = '{username}'");
                        Select($"update usersRequest set name = '{name}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "Спасибо за заявку, мы позвоним Вам в ближайшее время, чтобы обсудить условия ремонта.", replyMarkup: EmptyAnsver());
                        await SendMessage(username.ToString());
                        Select($"update usersRequest set counter = 4 where id = '{username}'");;

                        break;
                    }
                    if (ss.Text == "Вернуть налоговый вычет🏦" && Counter == 1 && Block == 4 || ss.Text == back && Counter == 6 && Block == 4)
                    {
                        Select($"update usersRequest set selectedProfession = '{ss.Text}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "Так как мы имеем лицензию в сфере образовательной деятельности Вы можете вернуть налоговый вычет 13％ от стоимости обучения.\nТак же, мы можем помочь, абсолютно бесплатно, подготовить для Вас полный пакет документов(3 НДФЛ Декларацию) для подачи в налоговую.", replyMarkup: request2Stage());
                        Select($"update usersRequest set counter = 5 where id = '{username}'"); ;

                        break;
                    }
                    if (ss.Text == "Оставить заявку" && Counter == 5 && Block == 4 || ss.Text == back && Counter == 7 && Block == 4)
                    {
                        await client.SendTextMessageAsync(ss.Chat.Id, "Введите номер телефона, чтобы мы могли связаться с Вами.", replyMarkup: EmptyAnsver());
                        Select($"update usersRequest set counter = 6 where id = '{username}'"); ;
                        break;
                    }
                    if (ss.Text != "" && Counter == 6 && Block == 4)
                    {
                        Select($"update usersRequest set phoneNumber = '{ss.Text}' where id = '{username}'");
                        Select($"update usersRequest set name = '{name}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "Спасибо за заявку, мы позвоним Вам в ближайшее время, чтобы обсудить условия возврата.", replyMarkup: EmptyAnsver());
                        await SendMessage(username.ToString());
                        Select($"update usersRequest set counter = 7 where id = '{username}'"); ;

                        break;
                    }
                    if (ss.Text == "Аренда конференц зала💵" && Counter == 1 && Block == 4 || ss.Text == back && Counter == 9 && Block == 4)
                    {
                        Select($"update usersRequest set selectedProfession = '{ss.Text}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "https://delfa72.ru/arenda-konfernts-zala/Prays_arenda.pdf\n"+
                            "Место проведения Зал№ 1 (2этаж)\nПонедельник - пятница\n9:00 - 18:00 18:00 - 23:00  \n1000 руб./ч 1500 руб./ч\n\nСуббота\nот 1ч до 5ч Свыше 5ч\n1500руб./ч 1300руб./ч \n\nВоскресенье \nот 1ч до 5ч Свыше 5ч\n1900руб./ч 1700руб./ч", replyMarkup: request2Stage());
                        Select($"update usersRequest set counter = 8 where id = '{username}'"); ;

                        break;
                    }
                    if (ss.Text == "Оставить заявку" && Counter == 8 && Block == 4 || ss.Text == back && Counter == 10 && Block == 4)
                    {
                        await client.SendTextMessageAsync(ss.Chat.Id, "Введите номер телефона, чтобы мы могли связаться с Вами.", replyMarkup: EmptyAnsver());
                        Select($"update usersRequest set counter = 9 where id = '{username}'"); ;
                        break;
                    }
                    if (ss.Text != "" && Counter == 9 && Block == 4)
                    {
                        Select($"update usersRequest set phoneNumber = '{ss.Text}' where id = '{username}'");
                        Select($"update usersRequest set name = '{name}' where id = '{username}'");
                        await client.SendTextMessageAsync(ss.Chat.Id, "Спасибо за заявку, мы позвоним Вам в ближайшее время, чтобы обсудить условия аренды.", replyMarkup: EmptyAnsver());
                        await SendMessage(username.ToString());
                        Select($"update usersRequest set counter = 10 where id = '{username}'"); ;

                        break;
                    }
                    break;
                }
            }
            catch (Exception ex)
            { }
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
        private static async Task SendMessage(string userId)
        {
            try
            {
            DataTable user = await Select($"select selectedProfession, phoneNumber, name from userrequest whete id = '{userId}'");

            string NameOfUser = "";
            string profession = "";
            string phone = "";

            for (int i = 0; i < user.Rows.Count; i++)
            {
                NameOfUser = user.Rows[i][2].ToString();
                profession = user.Rows[i][0].ToString();
                phone = user.Rows[i][1].ToString();
            }

                var wTLClient = new WTelegram.Client(Config);
                var my = await wTLClient.LoginUserIfNeeded();
                var resolved = await wTLClient.Contacts_ResolvePhone($"89199400273"); // username without the @
                await wTLClient.SendMessageAsync(resolved, $@"Пришел запрос на ""{requestProfession}"" от пользователя {requestName}{"\n"}Номер: {requestPhone}");
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
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Летняя IT-школа☀" },  new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Обучение для детей👶" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Обучение для взрослых👨" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Дополнительные услуги⚙" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "О нас❓" } }

                }
            };
        }
        private static IReplyMarkup Question2Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "5" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "4" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "3" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "2" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "1" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Начать заново" } }
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
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Начать заново" } }
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
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Начать заново" } }
                }
            };
        }
        private static IReplyMarkup EmptyAnsver()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Начать заново" } }
                }
            };
        }
        private static IReplyMarkup EmptyAnsver2()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {                   
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Не хочу!" }},
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Начать заново" } }
                }
            };
        }
        private static IReplyMarkup request1Stage()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Оставить заявку на занятие" }},
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Начать заново" } }
                }
            };
        }
        private static IReplyMarkup request2Stage()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Оставить заявку" }},
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Начать заново" } }
                }
            };
        }
        private static IReplyMarkup Question6Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Летняя IT-школа☀" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "3-6 Лет" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "1-4 Класс" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "5-11 Класс" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Начать заново" } }
                }
            };
        }
        private static IReplyMarkup Question7Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Направления" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Расписание смен📆" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Начать заново" } }
                }
            };
        }
        private static IReplyMarkup Question8Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Системное администрирование 12+" },  new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Play studio 8+" },  new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Школа видеоблогера 11+" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Школа GameDev 11+" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Мобильная фотография 11+" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Школа инжиниринга умных вещей 12+" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Начать заново" } }
                }
            };
        }
        private static IReplyMarkup Question9Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Контактные данные📕" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "График работы⏰" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Фотогалерея📷" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Начать заново" } }
                }
            };
        }
        private static IReplyMarkup Question10Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Аренда конференц зала💵" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Вернуть налоговый вычет🏦" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Ремонт компьютеров🔧" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = back }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Начать заново" } }
                }
            };
        }
        public static async Task<DataTable> Select(string selectSQL)
        {
            DataTable data = new DataTable("dataBase");

            string path = "ConnectionString.txt";

            string text = File.ReadAllText(path);

            string[] vs = text.Split('"');

            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection($"server = {vs[1]};Trusted_connection={vs[3]};DataBase={vs[5]};User={vs[7]};PWD={vs[9]}");
            sqlConnection.Open();

            System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;

            System.Data.SqlClient.SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(data);

            return data;
        }
    }
}