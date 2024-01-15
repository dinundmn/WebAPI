using Newtonsoft.Json;
using webapi.Model;

namespace webapi
{
    public class EmpHelpers
    {

        public static Employees? GetExistingEmployeeList(string path)
        {
            if (File.Exists(path))
            {
                var textData = File.ReadAllText(path);
                var existingEmp = JsonConvert.DeserializeObject<Employees>(textData);
                return existingEmp;
            }
            return null;
        }

        public static void SerializeEmpList(Employees emps, string _path)
        {
            var json = JsonConvert.SerializeObject(emps);
            System.IO.File.WriteAllText(_path, json);
        }
    }
}
