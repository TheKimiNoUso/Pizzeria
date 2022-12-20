using Pizzeria;
using Pizzeria.Customer;
using Pizzeria.Pizza;

Start();

void Start()
{
    var pizza = new Pizza();
    var welcome = new Welcome();
    var customer = new Customer();

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Welcome to the Pizzeria\n\n");
    Console.ResetColor();

    AddDebugUser();
    welcome.SendStartUpMessage();
}


void AddDebugUser()
{
    Customer.customer.Add(new Customer()
    {
        username = "Test",
        postcode = "SW1A 0AA",
        houseNumber = "10",
        phoneNumber = "07534000000"
    });
}