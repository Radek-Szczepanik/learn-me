using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LernMe.Models.Front
{
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public Category Category { get; set; }
        public int Amount { get; set; }
        public bool CheckBoxAnswer { get; set; }

    }
}
