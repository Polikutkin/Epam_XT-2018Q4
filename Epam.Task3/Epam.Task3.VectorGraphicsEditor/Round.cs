﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.VectorGraphicsEditor
{
    public class Round : Shape
    {
        public Round()
        {
        }

        public Round(double r, double x, double y)
        {
            if (r <= 0)
            {
                try
                {
                    throw new ArgumentException("Round with a radius equal to or less than 0 cannot exist.", nameof(r));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

            this.Radius = r;
            this.X = x;
            this.Y = y;
        }

        public override double X { get; set; }

        public override double Y { get; set; }

        public double Radius { get; }

        public double Length => 2 * Math.PI * this.Radius;

        public double Square => Math.PI * Math.Pow(this.Radius, 2);

        public override void ShowInfo()
        {
            Console.WriteLine();
            Console.WriteLine($"Shape: {this.GetType().Name}");
            Console.WriteLine($"Coordinate X: {this.X}");
            Console.WriteLine($"Coordinate Y: {this.Y}");
            Console.WriteLine($"Radius: {this.Radius}");
            Console.WriteLine($"Circumference: {this.Length}");
            Console.WriteLine($"Area: {this.Square}");
        }
    }
}