using System;
using System.Collections.Generic;
using System.Linq;

namespace AB
{
    public class AddressBook
    {
        private List<User> users = new List<User>();

        public void AddUser()
        {
            User user = new User();

            Console.WriteLine("Last name");
            while ((user.LastName = Console.ReadLine()) == "")
            {
                Console.WriteLine("Try again");
            }

            Console.WriteLine("First name");
            while ((user.FirstName = Console.ReadLine()) == "")
            {
                Console.WriteLine("Try again");
            }

            Console.WriteLine("Birthdate");
            string date = Console.ReadLine();
            DateTime dt;
            while (!DateTime.TryParse(date, out dt))
            {
                Console.WriteLine("Try again");
                date = Console.ReadLine();
            }
            user.Birthdate = DateTime.Parse(date);

            user.TimeAdded = DateTime.Now;

            Console.WriteLine("City");
            while ((user.City = Console.ReadLine()) == "")
            {
                Console.WriteLine("Try again");
            }

            Console.WriteLine("Address");
            while ((user.Address = Console.ReadLine()) == "")
            {
                Console.WriteLine("Try again");
            }

            Console.WriteLine("Phone number");
            while ((user.PhoneNumber = Console.ReadLine()) == "")
            {
                Console.WriteLine("Try again");
            }

            Console.WriteLine("Gender");
            while ((user.Gender = Console.ReadLine()) == "")
            {
                Console.WriteLine("Try again");
            }

            Console.WriteLine("Email");
            while ((user.Email = Console.ReadLine()) == "")
            {
                Console.WriteLine("Try again");
            }

            users.Add(user);

            if (UserAdded != null)
            {
                UserAdded("adding new user");
            }

        }

        public void RemoveUser()
        {
            Console.WriteLine("Write last name");

            users.Remove(users.Find(x => x.LastName == Console.ReadLine()));

            if (UserRemoved != null)
            {
                UserRemoved("removing user");
            }
        }

        public delegate void ABDelegate(string str);

        public event ABDelegate UserAdded;
        public event ABDelegate UserRemoved;

        public void AllUsers()
        {
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine(users[i].LastName + " " + users[i].FirstName + "\n");
            }
        }

        public IEnumerable<User> FindByEmail()
        {
            IEnumerable<User> gmail = users.Where(u => u.Email.Contains("gmail.com")).Select(u => u);

            return gmail;
        }

        public IEnumerable<User> GirlsTenDays()
        {
            IEnumerable<User> girls = from g in users
                                      where g.Gender == "woman" &&
                      g.TimeAdded <= DateTime.Now && g.TimeAdded >= DateTime.Now.AddDays(-10)
                                      select g;

            return girls;
        }

        public IEnumerable<User> FindJanuary()
        {
            IEnumerable<User> jan = users.Where(u => u.Birthdate.Month == 1 && u.Address != "" &&
                                    u.PhoneNumber != "").OrderByDescending(u => u.LastName);

            return jan;
        }

        public IEnumerable<User> BirthInCity(string city)
        {
            IEnumerable<User> u = from us in users
                    where us.City == city && us.Birthdate.Day == DateTime.Now.Day &&
                    us.Birthdate.Month == DateTime.Now.Month
                    select us;

            return u;
        }

        public IEnumerable<User> Kyiv18()
        {
            IEnumerable<User> kyiv18 = users.GetFromKyiv();

            return kyiv18;
        }

        public Dictionary<string, List<User>> ChooseDictionary()
        {
            Dictionary<string, List<User>> dictionary = new Dictionary<string, List<User>>();
            dictionary.Add("man", users.Where(m => m.Gender == "man").Select(m => m).ToList());
            dictionary.Add("woman", users.Where(w => w.Gender == "woman").Select(w => w).ToList());

            return dictionary;
        }

        public IEnumerable<User> Paging(Func<User, bool> lambda, int a, int b)
        {
            IEnumerable<User> page = users.Where(lambda).Skip(a - 1).Take(b - a + 1);

            return page;
        }

        public IEnumerable<string> Birthday()
        {
            IEnumerable<string> all = from u in users where u.Birthdate.Day == DateTime.Now.Day &&
                                    u.Birthdate.Month == DateTime.Now.Month select u.Email;

            return all;
        }
    }

    static class Extension
    {
        public static IEnumerable<User> GetFromKyiv(this IEnumerable<User> list)
        {
            foreach (User u in list)
            {
                if (u.City == "Kyiv" && DateTime.Now.Year - u.Birthdate.Year > 18)
                    yield return u;
            }
        }
    }
}
