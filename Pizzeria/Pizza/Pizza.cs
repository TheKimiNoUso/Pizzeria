namespace Pizzeria.Pizza
{
    internal class Pizza
    {
        public string[] pizzaToppings;
        public string pizzaSize;
        public float pizzaPrice;

        public void SetSize(string size)
        {
            pizzaSize = size;
        }

        public void SetToppings(string[] toppings)
        {
            pizzaToppings = toppings;
        }

        public void SetPrice(float price)
        {
            pizzaPrice = price;
        }
    }
}