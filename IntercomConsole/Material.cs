using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercomConsole
{
    [Serializable]
    internal class Material
    {
        public Material(string name, int price) 
        {
            Name = name;
            Price = price;
        }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
