using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Pizza
{
    internal class PizzaManager
    {
        private List<Pizza> pizzaList = new();
        public int Quantity { get; set; }


        public void AddPizza(Pizza pizza)
        {
            pizzaList.Add(pizza);
        }

        public float TotalPrice()
        {
            float total = 0;

            for (int i = 0; i < pizzaList.Count; i++)
            {
                total += pizzaList[i].pizzaPrice;
            }

            return total;
        }
    }
}