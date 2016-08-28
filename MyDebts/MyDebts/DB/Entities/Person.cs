using System;
using SQLite;

namespace MyDebts.Entities
{
    [Table("Person")]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public DateTime When { get; set; }
        public string Comment { get; set; }
        public bool MyDebt { get; set; }
    }
}   