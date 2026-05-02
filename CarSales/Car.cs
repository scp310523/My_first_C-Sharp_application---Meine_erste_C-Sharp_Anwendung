using System;

namespace CarSales
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
        public string Condition { get; set; }
        public int Age { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }

        public Car() { } // Required by CsvHelper

        public override string ToString()
        {
            return $"{ID} {Color} {Make} {Model} {Price}€";
        }
    }
}