using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales
{
    internal class CarWPF
    {
        public int ID { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Doors { get; set; }
        public int MinPS { get; set; }
        public int MaxPS { get; set; }
        public String Condition { get; set; }
        public int Age { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }
        //public Decimal PriceTotal { get; private set; }
        public Boolean IsSold { get; set; }
        public CarWPF(int id, string color, string make, string model, int doors, int minPS, int maxPS, string condition, int age, int mileage, decimal price, bool isSold)
        {
            ID = id;
            Color = color;
            Make = make;
            Model = model;
            Doors = doors;
            MinPS = minPS;
            MaxPS = maxPS;
            Condition = condition;
            Age = age;
            Mileage = mileage;
            Price = price;
            IsSold = isSold;
        }
        public CarWPF() { }

        public string toString()
        {
            return ID + " " + Color + " " + Make + " " + Model + " " + Price + "€";
        }
    }
}
