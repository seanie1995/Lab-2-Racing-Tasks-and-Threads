using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class Car
    {
        public string name {  get; set; }
        public int speed { get; set; }
        public int distanceTravelled { get; set; }

        public Car(string Name)
        {
            name = Name;
            speed = 100;
            distanceTravelled = 0;
        }


    }
}
