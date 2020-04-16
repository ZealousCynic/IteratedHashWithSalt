using System;

namespace HashWithSalt
{
    class Program
    {
        static int attempts = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Salted hasher booting up.. \n");

            Console.WriteLine("Please enter a number corresponding to your desired hash type.");

            HashWorker worker = new HashWorker();

            byte[] salt = GetSalt(worker);

            while (attempts <= 5)
                ChooseOperation(worker, salt);

            Console.WriteLine("Program terminating due to too many invalid access attempts.");
            Console.ReadKey();
            Environment.Exit(0xA0);
        }

        static byte[] GetSalt(HashWorker worker)
        {
            SqlSalt salter = new SqlSalt();

            byte[] salt = salter.GetSalt();

            if (salt == null)
            {
                Console.WriteLine("Salt was null! Generating new salt");
                salt = worker.GetSalt(16);
                salter.StoreSalt(Convert.ToBase64String(salt));
            }

            return salt;
        }

        static void ChooseOperation(HashWorker worker, byte[] salt)
        {
            int operation;

            Console.WriteLine("What operation would you like to perform?\n\n" +
                "0: Login\n" +
                "1: Create new user\n" +
                "2: Display known users\n" +
                "9: Exit Application\n\n");

            int.TryParse(Console.ReadLine(), out operation);

            switch (operation)
            {
                case 0:
                    Login(worker, salt);
                    break;
                case 1:
                    CreateUser(worker, salt);
                    break;
                case 2:
                    DisplayUsers();
                    break;
                case 9:
                    Console.WriteLine("Hashed salted closing down...");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        static void CreateUser(HashWorker worker, byte[] salt)
        {
            Console.WriteLine("Please enter the username to be stored...\n");
            string username = Console.ReadLine();
            Console.WriteLine("Please enter the password to be encrypted...\n");
            string password = Console.ReadLine();
            Console.WriteLine("Working...");

            byte[] hashedPW = worker.ComputeIteratedHash(password, salt);

            password = Convert.ToBase64String(hashedPW);

            using (UserStorage us = new UserStorage())
            {
                us.Create(new User { Username = username, Password = password });
            }
        }

        static void DisplayUsers()
        {
            User[] users;

            using (UserStorage us = new UserStorage())
            {
                users = us.GetAll();
            }

            foreach (User u in users)
            {
                Console.WriteLine(u.ToString());
            }
        }

        static void Login(HashWorker worker, byte[] salt)
        {
            Console.WriteLine("Please enter your username...\n");
            string username = Console.ReadLine();
            Console.WriteLine("Please enter your password...\n");
            string password = Console.ReadLine();
            Console.WriteLine("Working...");

            byte[] hashedPW = worker.ComputeIteratedHash(password, salt);

            password = Convert.ToBase64String(hashedPW);

            User u;

            using (UserStorage us = new UserStorage())
            {
                u = us.GetByPassword(password);
            }

            if (u is null)
            {
                attempts++;
                Console.WriteLine("Invalid credentials...");
                return;
            }

            Console.WriteLine(u.ToString());
            attempts = 0;
        }
    }
}
