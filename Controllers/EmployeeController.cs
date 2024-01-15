using Microsoft.AspNetCore.Mvc;
using webapi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        string _path = string.Empty;

        public EmployeeController() 
        {
            if (_path == string.Empty)
            {
                _path = Directory.GetCurrentDirectory();
                _path = Path.Combine(_path, "EmployeeList.json");
            }
        } 

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var emps = EmpHelpers.GetExistingEmployeeList(_path);
            return emps?.EmpDB.Select(x => x.Name);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var emps = EmpHelpers.GetExistingEmployeeList(_path);
            return emps.EmpDB.FirstOrDefault(x => x.Id == id).Name;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody]EmployeeAddPayload payload)
        {
            Employee emp = new Employee
            {
                Id = payload.Id,
                Name = payload.Name
            };

            var existingEmps = EmpHelpers.GetExistingEmployeeList(_path);

            if (existingEmps == null)
            {
                Employees emps = new();
                emps.EmpDB.Add(emp);

                EmpHelpers.SerializeEmpList(emps, _path);
            }
            else
            {
                existingEmps.EmpDB.Add(emp);
                EmpHelpers.SerializeEmpList(existingEmps, _path);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] EmployeeAddPayload payload)
        {
            var existingEmps = EmpHelpers.GetExistingEmployeeList(_path);

            if (existingEmps != null)
            {
                var result = existingEmps.EmpDB.Where(x => x.Id == payload.Id).FirstOrDefault();
                result.Name = payload.Name;

                EmpHelpers.SerializeEmpList(existingEmps, _path);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existingEmps = EmpHelpers.GetExistingEmployeeList(_path);
            Employee e = existingEmps.EmpDB[id];

            if (existingEmps != null)
            {
                existingEmps.EmpDB.Remove(e);
            }

        }
    }
}
