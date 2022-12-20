namespace Pizzeria.Customer
{
    internal class Customer
    {
        public static List<Customer> customer = new();
        public static List<string> activeCustomerData = new();
        public static List<Pizza.Pizza> order = new();

        public string username { get; set; }
        public string postcode { get; set; }
        public string houseNumber { get; set; }
        public string phoneNumber { get; set; }

        public void SaveCustomer(string username, string postcode, string houseNumber, string phoneNumber)
        {
            customer.Add(new Customer()
            {
                username = username,
                postcode = postcode,
                houseNumber = houseNumber,
                phoneNumber = phoneNumber
            });
        }

        public bool ValidateCustomer(string username)
        {
            return customer.Exists(x => x.username == username);
        }
    }
}