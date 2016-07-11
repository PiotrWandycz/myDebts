using System;

namespace MyDebts.Entities
{
    public class Person
    {
        public string Name { get; set; }
        public float Amount { get; set; }
        public DateTime When { get; set; }
        public string Comment { get; set; }
        public bool OwesMe { get; set; }
    }
}