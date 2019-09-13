using Microsoft.EntityFrameworkCore;
using Pool.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pool
{
    class Program
    {
        /// <summary>
        /// В данном приложении дополнительная автоматизация можеть быть достигнута 
        /// непосредственным исключением контакта клиента с администратором. 
        /// С помощью создания удаленного приложения.
        /// </summary>
        static Context context = new Context();
        static void Main(string[] args)
        {
            using (context)
            {
                //Создание новой БД или подключение к уже существующей.
                context.Database.EnsureCreated();

                var service = context.Services.ToList();
                var sub = context.Subscriptions.ToList();
                WorkLoad();
                
                //Ввод личных данных клиентом.
                #region Заполнение данных
                Console.WriteLine("Введите имя:");
                var Fname = Console.ReadLine();

                Console.WriteLine("Введите фамилию:");
                var Lname = Console.ReadLine();

                Console.WriteLine("Возраст:");
                byte age = Convert.ToByte(Console.ReadLine());

                Console.WriteLine("Номер телефона (Формат ввода: +7-xxx-xxx-xx-xx):");
                var numberPhone = Console.ReadLine();

                Console.WriteLine("Наличие справки от дермотолога 1 - да, 0 - нет:");
                byte certificate = Convert.ToByte(Console.ReadLine());
                if (certificate == 0)
                {
                    Console.WriteLine("Желаете получить справку от врача в нашем центре? 1 - да, 0 - нет:");
                    certificate = Convert.ToByte(Console.ReadLine());
                }
                #endregion

                //Выбор предостовляемых услуг, при наличии мед. справки.
                #region Услуги
                if (certificate == 0)
                {
                    var client = new List<Client>()
                    {
                        new Client() {FName = Fname, LName = Lname, Age = age, NumberPhone = numberPhone, MedicalExamination = certificate}
                    };
                    context.Clients.AddRange(client);
                }
                else
                {
                    Console.WriteLine("Выберите услугу:");
                    foreach (var services in service)
                    {
                        Console.WriteLine($"({services.ServiceId}) - {services.ServiceName} ");
                    }
                    int serviceId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Выберите абонимент:");
                    foreach (var subs in sub)
                    {
                        Console.WriteLine($"({subs.SubscriptionId}) - Коичество занятий: {subs.QuantityOfOccupation}, Время: {subs.Time}");
                    }
                    int subId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Способ оплаты 1 - единовременная, 0 - авансом:");
                    string payment = default;
                    int pay = Convert.ToInt32(Console.ReadLine());
                    if (pay == 1)
                    {
                        payment = "Оплачено";
                    }
                    else
                        payment = "Авансом";

                    var client = new List<Client>()
                    {
                        new Client() {FName = Fname, LName = Lname, Age = age, NumberPhone = numberPhone, MedicalExamination = certificate, Payment = payment, ServiceId = serviceId, SubscriptionId = subId}
                    };
                    context.Clients.AddRange(client);
                }
                context.SaveChanges();
                #endregion
            }
        }

        /// <summary>
        /// Данные по умолчанию.
        /// </summary>
        private static void DefaultData()
        {
            var service = new List<Service>
            {
                new Service() { ServiceName = "Плавание" },
                new Service() { ServiceName = "Водное поло" },
                new Service() { ServiceName = "Дайвинг" },
                new Service() { ServiceName = "Аквааэробика" },
            };

            var sub = new List<Subscription>
            {
                new Subscription() {QuantityOfOccupation = 1, Time = "1 час"},
                new Subscription() {QuantityOfOccupation = 1, Time = "2 часа"},
                new Subscription() {QuantityOfOccupation = 1, Time = "3 часа"},
                new Subscription() {QuantityOfOccupation = 4, Time = "1 месяц"},
                new Subscription() {QuantityOfOccupation = 12, Time = "3 месяца"},
                new Subscription() {QuantityOfOccupation = 24, Time = "6 месяцев"},
            };

            var client = new List<Client>
            {
                new Client() {FName = "Фродо", LName = "Беггинс", Age = 25, NumberPhone = "+7-911-321-22-34", MedicalExamination = 1, Payment = "Оплачено", ServiceId = 1, SubscriptionId = 1},
                new Client() {FName = "Перегринн", LName = "Тук", Age = 30, NumberPhone = "+7-982-234-55-66", MedicalExamination = 1, Payment = "Авансом", ServiceId = 2, SubscriptionId = 2},
                new Client() {FName = "Питер", LName = "Макдермотт", Age = 29, NumberPhone = "+7-952-111-22-33", MedicalExamination = 0},
            };

            context.Services.AddRange(service);
            context.Subscriptions.AddRange(sub);
            context.Clients.AddRange(client);
            context.SaveChanges();
        }

        /// <summary>
        /// Вывод информации о загруженности чаш в бассейне.
        /// </summary>
        private static void WorkLoad()
        {
            var bowl = context.Bowls.ToList();
            if (bowl != null)
            {
                foreach(var bowls in bowl)
                {
                    context.Entry(bowls).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }

            var rnd = new Random();
            bowl = new List<Bowl>()
            {
                new Bowl() { BowlId = 1, WorkLoad = rnd.Next(0, 14) },
                new Bowl() { BowlId = 2, WorkLoad = rnd.Next(0, 20) },
            };
            context.Bowls.AddRange(bowl);
            context.SaveChanges();

            foreach (var bowls in bowl)
            {
                Console.WriteLine($"{bowls.BowlId} чаша: загруженность {bowls.WorkLoad} человек.");
            }
        }
    }
}