using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meine_erste_C_Sharp_anwendung
{
    internal class FoodType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public FoodType(int id, string name) 
        {
            ID = id;
            Name = name;
        }
    }
}
