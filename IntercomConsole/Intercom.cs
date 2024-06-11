using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercomConsole
{
    [Serializable]
    internal abstract class Intercom
    {
        public Intercom(string caption, Shape shape, Material material, string color)
        {
            Caption = caption;
            Shape = shape.Name;
            Material = material.Name;
            Color = color;
            Price = shape.Price + material.Price;
        }
        public string Caption { get; set; }
        public string Shape { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }

    }
}
