﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryEmployeeService : IEmployeesService
    {
        List<EmployeeViewModel> _employees;

        public InMemoryEmployeeService()
        {
            _employees = new List<EmployeeViewModel> {
                new EmployeeViewModel
                {
                    Id = 1,
                    FirstName = "Иван",
                    SurName = "Иванов",
                    Patronymic = "Иванович",
                    Age = 22,
                    Position = "Начальник"
                },
                new EmployeeViewModel
                {
                    Id = 2,
                    FirstName = "Владислав",
                    SurName = "Петров",
                    Patronymic = "Иванович",
                    Age = 35,
                    Position = "Программист"
                }
            };
        }

        public IEnumerable<EmployeeViewModel> GetAll()
        {
            return _employees;
        }

        public EmployeeViewModel GetById(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }

        public void Commit()
        {
            //throw new NotImplementedException();
        }

        public void AddNew(EmployeeViewModel model)
        {
            model.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(model);
        }

        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee is null)
                return;

            _employees.Remove(employee);
        }

        public EmployeeViewModel UpdateEmployee(int id, EmployeeViewModel entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }

            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee is null)
            {
                throw new InvalidOperationException("Employee not exits");
            }

            // заполним поля модели
            employee.Age = entity.Age;
            employee.FirstName = entity.FirstName;
            employee.Patronymic = entity.Patronymic;
            employee.SurName = entity.SurName;
            employee.Position = entity.Position;

            return employee;
        }
    }
}