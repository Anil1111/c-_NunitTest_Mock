using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private IEmployeeStorage _storage;

        public EmployeeController(IEmployeeStorage storage)
        {
            _storage = storage;
        }

        public ActionResult DeleteEmployee(int id)
        {
            _storage.DeleteEmployee(id);
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }

    public interface IEmployeeStorage
    {
        void DeleteEmployee(int id);
    }

    public class DbEmployeeStorage : IEmployeeStorage
    {
        private readonly IEmployeeRepository _repository;

        public DbEmployeeStorage(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public void DeleteEmployee(int id)
        {
           var dbEmployee = _repository.Find(id);
           if (dbEmployee == null)
           {
               return;
           }
           _repository.Remove(dbEmployee);
           _repository.SaveChanges();
        }
    }

    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Employees { get; set; }
        Employee Find(int id);
        void Remove(Employee employee);
        void SaveChanges();
    }

    public class Employee
    {
    }
}