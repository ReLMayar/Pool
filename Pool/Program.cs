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
                if(service.Count == 0 && sub.Count == 0)
                {
                    DefaultData(out service, out sub);
                }

                List<Client> client;
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
                    client = new List<Client>()
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
                    int payment = Convert.ToInt32(Console.ReadLine());
                    if (payment == 1)
                    {
                        client = new List<Client>()
                        {
                            new Client() {FName = Fname, LName = Lname, Age = age, NumberPhone = numberPhone, MedicalExamination = certificate, Payment = "Оплачено", ServiceId = serviceId, SubscriptionId = subId}
                        };
                    }
                    else
                    {
                        client = new List<Client>()
                        {
                            new Client() {FName = Fname, LName = Lname, Age = age, NumberPhone = numberPhone, MedicalExamination = certificate, Payment = "Авансом", ServiceId = serviceId, SubscriptionId = subId}
                        };
                    }
                    context.Clients.AddRange(client);
                }
                context.SaveChanges();
                #endregion
            }
        }

        /// <summary>
        /// Данные по умолчанию.
        /// </summary>
        private static void DefaultData(out List<Service> service, out List<Subscription> sub)
        {
            service = new List<Service>
            {
                new Service() { ServiceId = 1, ServiceName = "Плавание" },
                new Service() { ServiceId = 2, ServiceName = "Водное поло" },
                new Service() { ServiceId = 3, ServiceName = "Дайвинг" },
                new Service() { ServiceId = 4, ServiceName = "Аквааэробика" },
            };

            sub = new List<Subscription>
            {
                new Subscription() { SubscriptionId = 1, QuantityOfOccupation = 1, Time = "1 час"},
                new Subscription() { SubscriptionId = 2, QuantityOfOccupation = 1, Time = "2 часа"},
                new Subscription() { SubscriptionId = 3, QuantityOfOccupation = 1, Time = "3 часа"},
                new Subscription() { SubscriptionId = 4, QuantityOfOccupation = 4, Time = "1 месяц"},
                new Subscription() { SubscriptionId = 5, QuantityOfOccupation = 12, Time = "3 месяца"},
                new Subscription() { SubscriptionId = 6, QuantityOfOccupation = 24, Time = "6 месяцев"},
            };

            context.Services.AddRange(service);
            context.Subscriptions.AddRange(sub);
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