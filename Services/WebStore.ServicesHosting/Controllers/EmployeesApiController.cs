using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/employees")]
    //[ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesService
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesApiController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet, ActionName("Get")]
        public IEnumerable<EmployeeViewModel> GetAll()
        {
            return _employeesService.GetAll();
        }

        [HttpGet("{id}"), ActionName("Get")]
        public EmployeeViewModel GetById(int id)
        {
            return _employeesService.GetById(id);
        }

        public void Commit()
        {
        }

        [HttpPost, ActionName("Post")]
        public void AddNew([FromBody]EmployeeViewModel model)
        {
            _employeesService.AddNew(model);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeesService.Delete(id);
        }

        [HttpPut, ActionName("Put")]
        public EmployeeViewModel UpdateEmployee(int id, [FromBody]EmployeeViewModel entity)
        {
            return _employeesService.UpdateEmployee(id, entity);
        }
    }
}
