using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
namespace CoffeeShop
{
    class CoffeeShopMenu
    {
        private CupOfCoffee coffee;// = new CupOfCoffee();
        private Sandwich sandwich;
        private Pastry pastry;
        string fileName = "MenuItem.txt";
        private string[] menuItems;

       // private const int EXIT = 8; //can't use this any more

        //Added the constructor to make the objects and arrays
        public CoffeeShopMenu()
        {
            //string[] coffeeOptions = {"Brown Sugar", "Molasses", "Honey",
            //"Chocolate Chips", "Whipped Cream", "Mint", "Marshmellows", "Ice Cream" };
            //double[] coffeeAddedPrice = { 0.25, 0.20, 0.1, 0.3, 0.15, 0.17, 0.05, 1.25 };
            //string[] sandwichOptions = {"Bacon", "Egg", "Cheese", "Chicken","Tuna","Tomato","Lettice",
            //"Pickles", "Peanut Butter", "Jelly", "Avacado"};
            //double[] sandwichAddedPrice = { 0.59, 0.21, 0.32, 1.05, 0.75, 0.22, 0.05, 0.19,
            //    0.20, 0.18, 0.31 };

            //coffee = new CupOfCoffee("coffee", coffeeOptions, coffeeAddedPrice);
            //sandwich = new Sandwich("sandwich", sandwichOptions, sandwichAddedPrice);

        }

        public void ReadFromFile() 
        {
            string newItem = "**";
            string[] lines = new string[100];
            string[] menuCategoryNames = new string[50];
            int count = 0, current = 0;

            

            if (File.Exists(fileName)) 
            {
                StreamReader filStream = new StreamReader(fileName);
                while ((menuCategoryNames[current] = filStream.ReadLine()) != null)// get the name of the menu item 
                {
                    count = 0;
                    lines[count] = filStream.ReadLine();

                    while ((lines[count] != newItem)) //read in all of the options and cost 
                    {
                        lines[++count] = filStream.ReadLine();
                    }

                    string[] fileOptions = new string[count - 1];
                    double[] fileAddedPrice = new double[count - 1];
                    for (int index = 0; index < fileOptions.Length; index++) 
                    {
                        string[] pair = lines[index].Split(',');

                        fileOptions[index] = pair[0];
                        double.TryParse(pair[1], out fileAddedPrice[index]);    
                    }
                    CreateItemObject(menuCategoryNames[current++], fileOptions, fileAddedPrice);
                }
            }
        }

        public bool CreateItemObject(string menuCategoryNames, string[] options, double[] addedPrice) 
        {
            bool created = true;
            switch (menuCategoryNames) 
            {
                case "coffe":
                    coffee = new CupOfCoffee(menuCategoryNames, options, addedPrice);
                    break;
                case "sandwich":
                    sandwich = new Sandwich(menuCategoryNames, options, addedPrice);
                    break ;
                case "pastry":
                    pastry = new Pastry(menuCategoryNames, options, addedPrice, 20);
                    break;
                default:
                    created = false;
                    break;
            }
            return created;
        }


        
        public int DisplayMenu(Item item) //change added parameter
        {
            Clear();
            int selection = 0;
            WriteLine("{0} Topping Options\nSelect:\n", item.Name); //change --WriteLine("Coffee Topping Options\nSelect:\n");

            string[] options = item.Options; // coffee.Options;
            double[] cost = item.AddedPrice; // coffee.AddedPrice;

            for (int i = 0; i < options.Length; i++)
            {
                WriteLine("\t{0}.  {1,-20} ${2,7:F2}", (i + 1), options[i], cost[i]);
            }
            WriteLine("\t{0}.  {1,-20}", options.Length + 1, "Exit");
            selection = ReadInChoice(1, options.Length + 1);

            return (selection - 1);

        }


        public int ReadInChoice(int lowerLimit, int upperLimit)
        {
            string inValue;
            int number;
            bool numChecked = false;
            do
            {
                inValue = ReadLine();
                while (int.TryParse(inValue, out number) == false)
                {
                    WriteLine("Incorrect entry.");
                    WriteLine("Please inter a numerical entry");
                    inValue = ReadLine();
                }
                if ((number < lowerLimit) || (number > upperLimit))
                {
                    WriteLine("Incorrect entry.");
                    WriteLine("Select a number between {0} and {1}",
                        lowerLimit, upperLimit);
                }
                else
                {
                    numChecked = true;
                }
            } while (numChecked == false);

            return number;
        }

        public bool UpdateItem(Item item, string topping) //changed ---Coffee(string topping)
        {
            bool completed = false;
            string addRemove;

            completed = item.ModifyItem(topping, out addRemove); //changed-- coffee.ModifyCoffee(topping, out addRemove);

            if (completed)
            {
                WriteLine("{0}:  {1} {2} topping", item.Name, addRemove, topping); //changed -- WriteLine("Coffee:  {0} {1} topping", addRemove, topping);
            }
            else
            {
                WriteLine("Error: Unable to modify item."); //changed -- coffee.");
            }

            return completed;
        }

        public void DisplayToppings(Item item) //changed, added item
        {
            WriteLine("{0} ${1:F2}", item.Name, item.Cost); //changed -- WriteLine("Coffee ${0:F2}", coffee.Cost);
            WriteLine("\nSelected Toppings:\n");
            foreach (string topping in item.Toppings) //changed -- coffee.Toppings)
            {
                WriteLine("\t{0, -20} ${1,7:F2}", topping, item.getPrice(topping));// changed -- coffee.getPrice(topping)) ;  ;
            }
            WriteLine("\n");
            WriteLine("Total Cost ${0:F2}", item.CalcCost()); //changed -- coffee.CalcCost()) ;
        }

        //Added below method for inheritance 
        public void SelectItem()
        {
            Clear();
            WriteLine("(1) Coffee");
            WriteLine("(2) Sandwich");
            WriteLine("(3) Check Out");
            int selection = ReadInChoice(1, 3);
            switch (selection)
            {
                case 1:
                    MainMenu(coffee);
                    break;
                case 2:
                    string state = sandwich.Toasted ? "Sandwich will be toasted" : "Sandwich will not be toasted";
                    WriteLine(state);
                    WriteLine("Enter T to change");
                    char T = (char)Read();
                    if (T == 'T') { sandwich.Toasted = !sandwich.Toasted; }

                    MainMenu(sandwich);
                    break;
                case 3:
                    DisplayToppings(coffee);
                    DisplayToppings(sandwich);
                    break;
            }
        }

        public void MainMenu(Item item) //change add item
        {
            int index = 0;
            string topping = "";

            while ((index = DisplayMenu(item)) != item.Options.Length) //change ---DisplayMenu()) != EXIT)
            {
                topping = item.option(index); //change -- coffee.option(index);
                UpdateItem(item, topping); // changed --- UpdateCoffee(topping);
                WriteLine("\nEnter any key to continue.");
                ReadKey();
            }
            SelectItem();
            //DisplayToppings(item); //change --- DisplayToppings();
        }
    }
}
