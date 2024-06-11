using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercomConsole
{
    [Serializable]
    internal class Shape
    {
        public Shape(string name, int price)
        {
            Name = name;
            Price = price;
        }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
