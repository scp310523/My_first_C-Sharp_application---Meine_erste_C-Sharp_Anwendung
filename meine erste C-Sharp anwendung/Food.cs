using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meine_erste_C_Sharp_anwendung
{
    internal class Food
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public Food(int id, string name, string description, decimal price)
        {
            ID = id;
            Name = name;
            Description = description;
            Price = price;
        }

        public Food(int id, string name, decimal price)
        {
            ID = id;
            Name = name;
            Price = price;
        }
    }
}