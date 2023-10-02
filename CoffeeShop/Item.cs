using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections; //needed for ArrayList
namespace CoffeeShop
{
    public abstract class Item
    {
        //private const double BASEPRICE = 2.15; //must be set in each child separate
        private ArrayList toppings = new ArrayList();
        private string[] options;
        private double[] addedPrice;
        private string name;

        public Item(string name, string[] options, double[] addedPrice)
        {
            this.name = name;
            this.options = options;
            this.addedPrice = addedPrice;
        }

        public abstract double Cost { get; }
        //{
        //   get { return BASEPRICE; }
        //}

        public string Name
        {
            get { return name; }
        }
        public string[] Options
        {
            get { return options; }
        }

        public double[] AddedPrice
        {
            get { return addedPrice; }
        }

        public ArrayList Toppings
        {
            get { return toppings; }
        }

        public string option(int index)
        {
            string selected = "";
            if ((index >= 0) && (index < options.Length))
            {
                selected = options[index];
            }
            return selected;
        }

        public double getPrice(string topping)
        {
            int link = FindIndex(topping);
            return (link == -1 ? 0 : addedPrice[link]);
        }

        public bool ModifyItem(string selectedTopping, out string addRemove)
        {
            addRemove = "";
            bool completed = false;

            bool validMenuSelection = options.Contains(selectedTopping);
            bool selectionAddedToCoffee = toppings.Contains(selectedTopping);

            if (validMenuSelection)
            {
                if (selectionAddedToCoffee)
                {
                    toppings.Remove(selectedTopping);
                    addRemove = "remove";
                }
                else
                {
                    toppings.Add(selectedTopping);
                    addRemove = "add";
                }
                completed = true;
            }

            return completed;
        }


        public virtual double CalcCost() //virtual allows it to be overriden
        {
            double total = 0;//= BASEPRICE;
            double addition = 0;
            int link;

            foreach (string topping in toppings)
            {
                link = FindIndex(topping);
                addition = link == -1 ? 0 : addedPrice[link];
                total += addition;
            }
            return total;

        }

        private int FindIndex(string topping)
        {

            int found = -1;
            for (int index = 0; index < options.Length; index++)
            {
                if (options[index] == topping)
                {
                    found = index;
                    index = options.Length;
                }
            }
            return found;
        }
    }
}
