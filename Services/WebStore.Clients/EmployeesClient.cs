using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Clients
{
    public class EmployeesClient : BaseClient, IEmployeesService
    {
        protected override string ServiceAddress { get; } = "api/employees";

        public EmployeesClient(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<EmployeeViewModel> GetAll()
        {
            var url = ServiceAddress;
            var list = Get<List<EmployeeViewModel>>(url);
            return list;
        }

        public EmployeeViewModel GetById(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            var result = Get<EmployeeViewModel>(url);
            return result;
        }
 
        public EmployeeViewModel UpdateEmployee(int id, EmployeeViewModel entity)
        {
            var url = $"{ServiceAddress}/{id}";
            var response = Put(url, entity);
            var result = response.Content.ReadAsAsync<EmployeeViewModel>().Result;
            return result;
        }
 
        public void AddNew(EmployeeViewModel model)
        {
            string url = $"{ServiceAddress}";
            Post<EmployeeViewModel>(url, model);
        }
 
        public void Delete(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            Delete(url);
        }
 
        public void Commit()
        {
        }

    }
}
