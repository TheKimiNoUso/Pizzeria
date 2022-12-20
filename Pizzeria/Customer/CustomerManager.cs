using System.Text.RegularExpressions;

namespace Pizzeria.Customer
{
    internal class CustomerManager
    {
        public void Login()
        {
            string existingCustomer;

            do
            {
                Console.WriteLine("\nAre you an existing customer? (y/n): ");
                existingCustomer = Console.ReadLine();
            } while (!Regex.IsMatch(existingCustomer, "[yn]"));

            if (existingCustomer.Equals("y"))
            {
                LoadUser();
            }
            else
            {
                RegisterUser();
            }
        }

        private void RegisterUser()
        {
            Customer customer = new();
            string username;
            string postcode;
            string houseNumber;
            string phoneNumber;

            do
            {
                Console.WriteLine("What would you like your username to be?");
                Suggestion("Your username must be alphanumeric & A maximum of 10 characters!");
                username = Console.ReadLine();
            } while (!Regex.IsMatch(username, "[A-Za-z0-9]") && username.Length >= 10);

            do
            {
                Console.WriteLine("What is your postcode?");
                Suggestion("E.g. SW1A 0AA!");
                postcode = Console.ReadLine();
            } while (!Regex.IsMatch(postcode, "^[a-zA-Z]{1,2}([0-9]{1,2}|[0-9][a-zA-Z])\\s*[0-9][a-zA-Z]{2}$"));

            do
            {
                Console.WriteLine("What is your house number?");
                Suggestion("E.g. 10!");
                houseNumber = Console.ReadLine();
            } while (!Regex.IsMatch(houseNumber, "[1-9]\\d*(\\s*[-/]\\s*[1-9]\\d*)?(\\s?[a-zA-Z])?"));

            do
            {
                Console.WriteLine("What is your phone number?");
                Suggestion("This must be a mobile phone number!");
                Console.Write("+44 ");
                phoneNumber = "+44" + Console.ReadLine();
            } while (!Regex.IsMatch(phoneNumber, "[0-9]{10}"));

            customer.SaveCustomer(username, postcode, houseNumber, phoneNumber);
            Console.WriteLine("Registration complete. Hello, " + username + "!");
            Login();
        }

        private void LoadUser()
        {
            Order order = new();
            Customer customer = new();
            string username;

            do
            {
                Console.WriteLine("What is your username?");
                Suggestion("To exit to main menu, type \"exit\".");
                username = Console.ReadLine();
                if (username.Equals("exit"))
                {
                    Login();
                }
            } while (!customer.ValidateCustomer(username));

            order.LoadOrderingUser(username);
        }

        public void Suggestion(String suggestion)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(suggestion);
            Console.ResetColor();
        }
    }
}