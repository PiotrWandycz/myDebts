using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyDebts.Entities;
using SQLite;

namespace MyDebts.DB.DataServices
{
    class PersonDataService
    {
        string _dbPath = null;
        SQLiteConnection _db = null;

        public PersonDataService()
        {
            _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "myDebtsDB.sqlite");
            _db = new SQLiteConnection(_dbPath);
            _db.CreateTable<Person>();
        }

        public IEnumerable<Person> GetPeople()
        {
            return _db.Table<Person>();
        }

        public Person GetPerson(int id)
        {
            return _db.Table<Person>().FirstOrDefault(x => x.Id == id);
        }

        public int GetNextId()
        {
            if (_db.Table<Person>().Count() == 0)
                return 0;
            return _db.Table<Person>().OrderBy(x => x.Id).Last().Id + 1;
        }

        public void AddUpdatePerson(Person person)
        {
            _db.InsertOrReplace(person);
        }

        public void DeletePerson(int id)
        {
            _db.Table<Person>().Delete(x => x.Id == id);
        }
    }
}