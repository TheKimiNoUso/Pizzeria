using System.Text.RegularExpressions;
using Pizzeria.Pizza;

namespace Pizzeria.Customer
{
    internal class Order
    {
        public static string username;
        public static string postcode;
        public static string houseNumber;
        public static string phoneNumber;

        public void LoadOrderingUser(string user)
        {
            CustomerManager customerManager = new();
            Customer customer = new();
            Order order = new();
            PizzaManager pizzaManager = new();
            Pizza.Pizza pizza = new();


            if (Customer.customer.Exists(x => x.username == user))
            {
                Customer validCustomer = Customer.customer.Find(match: x => x.username == user);

                username = user;
                postcode = validCustomer.postcode;
                houseNumber = validCustomer.houseNumber;
                phoneNumber = validCustomer.phoneNumber;
            }

            if (username != null && postcode != null && houseNumber != null && phoneNumber != null)
            {
                order.Start(pizzaManager);
            }
            else
            {
                Console.WriteLine(username, postcode, houseNumber, phoneNumber);
                customerManager.Login();
            }
        }

        private void Start(PizzaManager pizzaManager)
        {
            string options;

            do
            {
                Console.WriteLine(
                    "\nHow can I help you?\n1 - Add a Pizza to your order\n2 - Check how much your total is");
                options = Console.ReadLine();
            } while (!Regex.IsMatch(options, "[1-2]"));

            switch (options)
            {
                case "1":
                    AddNewPizza(pizzaManager);
                    break;
                case "2":
                    Checkout(pizzaManager);
                    break;
            }
        }

        private void AddNewPizza(PizzaManager pm)
        {
            Pizza.Pizza pizza = new Pizza.Pizza();
            CustomerManager customerManager = new CustomerManager();

            List<string> toppingsList = new List<string>();
            string toppingsAmount;
            string chosenSize = "UNKNOWN", sizeOption, toppingsOption;

            do
            {
                Console.WriteLine("What size would you like your Pizza?\n\n1 - SMALL\n2 - MEDIUM\n3 - LARGE");
                sizeOption = Console.ReadLine();
            } while (!Regex.IsMatch(sizeOption, "[1-3]"));

            switch (sizeOption)
            {
                case "1":
                    chosenSize = "SMALL";
                    break;
                case "2":
                    chosenSize = "MEDIUM";
                    break;
                case "3":
                    chosenSize = "LARGE";
                    break;
            }

            do
            {
                Console.WriteLine("How many toppings would you like?");
                customerManager.Suggestion("You may choose between one and five toppings.");
                toppingsAmount = Console.ReadLine();
            } while (!Regex.IsMatch(toppingsAmount, "[1-5]"));

            AcknowledgeOrder("You chose " + toppingsAmount + " toppings for this pizza");

            do
            {
                Console.WriteLine(
                    "What toppings would you like?\n\n1 - Pepperoni\n2 - Sausage\n3 - Pineapple\n4 - Ham\n5 - Red & Green Bellpepper");
                toppingsOption = Console.ReadLine();

                switch (toppingsOption)
                {
                    case "1":
                        toppingsList.Add("Pepperoni");
                        break;
                    case "2":
                        toppingsList.Add("Sausage");
                        break;
                    case "3":
                        toppingsList.Add("Pineapple");
                        break;
                    case "4":
                        toppingsList.Add("Ham");
                        break;
                    case "5":
                        toppingsList.Add("Red & Green Bellpepper");
                        break;
                }
            } while ((toppingsList.Count() != int.Parse(toppingsAmount)));

            pizza.SetToppings(toppingsList.ToArray());
            pizza.SetSize(chosenSize);
            pizza.SetPrice(CalculatePrice(toppingsList.Count(), int.Parse(sizeOption)));
            pm.AddPizza(pizza);
            AcknowledgeOrder("Successfully added a pizza to your order.");
            Console.WriteLine("Toppings:");
            for (int i = 0; i < toppingsList.Count; i++)
            {
                Console.WriteLine(" - " + toppingsList[i]);
            }

            Console.WriteLine("\nSize: " + pizza.pizzaSize + "\nPrice: £" + pizza.pizzaPrice + "\n");
            Start(pm);
        }

        private float CalculatePrice(int toppings, int size)
        {
            float total = 0;

            switch (size)
            {
                case 1:
                    total = 8F;
                    break;
                case 2:
                    total = 12F;
                    break;
                case 3:
                    total = 16F;
                    break;
            }

            total += (toppings * 0.75F);

            return total;
        }

        private void Checkout(PizzaManager pm)
        {
            string options;
            Welcome welcome = new Welcome();
            Random random = new Random();
            float total = pm.TotalPrice();

            Console.WriteLine("£" + total);

            do
            {
                Console.WriteLine("\n1 - Add more to your order\n2 - Confirm & Pay");
                options = Console.ReadLine();
            } while (!Regex.IsMatch(options, "[1-2]"));

            switch (options)
            {
                case "1":
                    Start(pm);
                    break;
                case "2":
                    if (total > 0)
                    {
                        AcknowledgeOrder("Thank you for your purchase! Come again soon.");
                        Console.WriteLine("Delivering to: No. " + houseNumber + ", " + postcode +
                                          ".\n Expected wait is currently " + random.Next(20, 50) + " minutes.");
                        Thread.Sleep(3000);
                        welcome.SendStartUpMessage();
                    }
                    else
                    {
                        Warn("Your basket is empty!");
                        Start(pm);
                    }

                    break;
            }
        }

        private void AcknowledgeOrder(String message)
        {
            Console.Beep();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private void Warn(String message)
        {
            Console.Beep();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}