using CsvHelper.Configuration.Attributes;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Car_Sale
{
    internal class Car
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



        public Car(int id, string color, string make, string model, int doors, int minPS, int maxPS, decimal price)
        {
            ID = id;
            Color = color;
            Make = make;
            Model = model;
            Doors = doors;
            MinPS = minPS;
            MaxPS = maxPS;
            Price = price;
        }
        public Car(String make, String condition, String model, int age, int mileage, Decimal price)
        {
            Make = make;
            Condition = condition;
            Model = model;
            Age = age;
            Mileage = mileage;
            Price = price;

        }

    }
}
