namespace webapi.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Employees
    {
        public List<Employee> EmpDB { get; set; } = new List<Employee>();
    }
}
