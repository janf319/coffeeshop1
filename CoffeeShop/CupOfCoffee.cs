using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    public class CupOfCoffee : Item
    {
        private const double BASEPRICE = 2.15;

        public CupOfCoffee(string name, string[] options, double[] addedPrice) : base(name, options, addedPrice) { }

        public override double Cost
        {
            get { return BASEPRICE; }
        }

        public override double CalcCost()
        {
            double total = BASEPRICE + base.CalcCost();
            return total;
        }

    }
}
