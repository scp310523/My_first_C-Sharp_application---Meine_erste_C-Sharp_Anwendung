using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sale
{
    internal class TradeType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public TradeType(int id, string name)
        {
            ID = id;
            Name = name;

        }
    }
}   
