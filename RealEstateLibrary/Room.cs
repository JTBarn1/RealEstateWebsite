using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    public class Room
    {
        private int length;
        private int width;
        private string description;
        private int squareFeet;

        public int Length { get { return length; } set { length = value; squareFeet = length * width; } }
        public int Width { get { return width; } set { width = value; squareFeet = length * width; } }
        public string Description { get { return description; } set { description = value; } }
        public int SquareFeet { get { return squareFeet; } }

        public Room(int length, int width,string desc)
        {
            this.length = length;
            this.width = width;
            this.description = desc;

            squareFeet = length*width;
        }
    
    }
}
