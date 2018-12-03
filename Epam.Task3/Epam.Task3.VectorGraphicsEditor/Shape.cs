using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.VectorGraphicsEditor
{
    public abstract class Shape
    {
        public abstract double X { get; set; }

        public abstract double Y { get; set; }

        public abstract void ShowInfo();
    }
}
