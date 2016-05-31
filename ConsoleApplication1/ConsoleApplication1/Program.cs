using System;
using AB;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            AddressBook book = new AddressBook();

            DateTime notifier = DateTime.Now.AddDays(-1); // Notifier contains last date we sent email

            string answer;

            do
            {
                Console.WriteLine("Add user - A, delete user - D, show all users - L, quit - Q");

                Console.WriteLine("Email contains 'gmail.com' - 1");
                Console.WriteLine("Years > 18 and city = Kyiv - 2");
                Console.WriteLine("Girls added in the last 10 days - 3");
                Console.WriteLine("January. Address and phone number not null. Decs by last name - 4");
                Console.WriteLine("Dictionary - 5");
                Console.WriteLine("Paging - 6");
                Console.WriteLine("Birthday from certain city - 7");
                Console.WriteLine("Use Notifier - 8");

                answer = Console.ReadLine();

                switch (answer.ToUpper())
                {
                    case "1":
                        {
                            IEnumerable<User> users = book.FindByEmail();
                            foreach (var user in users)
                            {
                                Console.WriteLine(user.LastName + " " + user.FirstName);
                            }
                        }
                        break;
                    case "2":
                        {
                            IEnumerable<User> users = book.Kyiv18();
                            foreach (var user in users)
                            {
                                Console.WriteLine(user.LastName + " " + user.FirstName);
                            }
                        }
                        break;
                    case "3":
                        {
                            IEnumerable<User> users = book.GirlsTenDays();
                            foreach (var user in users)
                            {
                                Console.WriteLine(user.LastName + " " + user.FirstName);
                            }
                        }
                        break;
                    case "4":
                        {
                            IEnumerable<User> users = book.FindJanuary();
                            foreach (var user in users)
                            {
                                Console.WriteLine(user.LastName + " " + user.FirstName);
                            }
                        }
                        break;
                    case "5":
                        {
                            Dictionary<string, List<User>> dict = book.ChooseDictionary();

                            List<User> men;
                            dict.TryGetValue("man", out men);
                            Console.WriteLine("Men: " + men.Capacity);
                        }
                        break;
                    case "6":
                        {
                            IEnumerable<User> users = book.Paging(u => u.Gender == "man", 2, 4);
                            foreach (var user in users)
                            {
                                Console.WriteLine(user.LastName + " " + user.FirstName);
                            }
                        }
                        break;
                    case "7":
                        {
                            IEnumerable<User> users = book.BirthInCity("Lviv");
                            foreach (var user in users)
                            {
                                Console.WriteLine(user.LastName + " " + user.FirstName);
                            }
                        }
                        break;
                    case "8":
                        {
                            if (notifier != DateTime.Now)
                            {
                                IEnumerable<string> emails = book.Birthday();
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.Port = 587;

                                smtp.Credentials = new NetworkCredential(
                                    "username@domain.com", "password");
                                smtp.EnableSsl = true;
                                MailAddress from = new MailAddress("Mail from");

                                foreach (string str in emails)
                                {
                                    MailAddress to = new MailAddress(str);
                                    MailMessage mail = new MailMessage(from, to);
                                    mail.Subject = "Birthday";
                                    mail.Body = "Happy Birthday!!!";
                                    smtp.Send(mail);
                                }
                                notifier = DateTime.Now;
                            }
                            else
                            {
                                Console.WriteLine("All greetings are sent.");
                            }
                        }
                        break;
                    case "A":
                        {
                            book.AddUser();
                            break;
                        }
                    case "D":
                        {
                            book.RemoveUser();
                            break;
                        }
                    case "L":
                        {
                            book.AllUsers();
                            break;
                        }
                    case "K":
                        book.Paging(u => u.Gender == "man", 2, 4);
                        break;
                    case "Q":
                        Console.WriteLine("Exiting..");
                        break;
                    default:
                        Console.WriteLine("Wrong input. Try again");
                        break;
                }

            }
            while (answer.ToLower() != "q");
        }
    }
}
