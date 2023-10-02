using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    public class Sandwich : Item
    {
        private const double BASEPRICE = 3.50;
        private bool toasted = false; //just for sandwich

        public Sandwich(string name, string[] options, double[] addedPrice) : base(name, options, addedPrice) { }

        public bool Toasted  //just for sandwich
        {
            get { return toasted; }
            set { toasted = value; }
        }

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
