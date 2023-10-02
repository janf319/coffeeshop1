using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    public class Pastry : Item
    {
        private const double BASEPRICE = 0;
        private double discount;

        public Pastry(string name, string[] options, double[] addPrice, double discount) : base(name, options, addPrice) 
        {
            this.discount = discount;
        }

        public override double Cost 
        {
            get { return BASEPRICE; }
        }
        public override double CalcCost() 
        {
            double total = base.CalcCost();
            if (Toppings.Count > 1) 
            {
                total = total *(1 - discount);
            }
            return total;
        }
    }
}
