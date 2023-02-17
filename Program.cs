using System.Collections;
using System.Linq;

namespace Linq
{
    public class Program
    {
        IList employeeList;
        IList salaryList;

        public Program()
        {
            employeeList = new List<Employee> {
                new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
                new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
                new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
                new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
                new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
                new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
                new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
            };

            salaryList = new List<Salary> {
                new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
                new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
                new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
                new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
                new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
                new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
                new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
                new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
                new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
                new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
                new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
                new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
                new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
            };

        }

        public static void Main()
        {

            Program program = new Program();

            program.printEmpSalary();    // task 1


            program.Find2ndOldestEmp();

            program.CalculateMean();
        }
        public void printEmpSalary()
        {
            // join employee and salary list on employee id based on monthly salary, and sort by salary in ascending order.
            var resultList = from emp in employeeList.Cast<Employee>() // cast the list to Employee type
                             join sal in salaryList.Cast<Salary>() on emp.EmployeeID equals sal.EmployeeID
                             where sal.Type == SalaryType.Monthly
                             orderby sal.Amount ascending
                             select new { emp.EmployeeID, emp.EmployeeFirstName, emp.EmployeeLastName, sal.Amount };

            // print the result
            System.Console.WriteLine("---------------------------------Task 1---------------------------------");
            Console.WriteLine("Employee ID\tEmployee Full Name\tSalary\n");
            foreach (var emp in resultList)
            {
                Console.WriteLine(emp.EmployeeID + "\t\t" + emp.EmployeeFirstName + " " + emp.EmployeeLastName + "\t\t" + emp.Amount);

            }

        }

        public void Find2ndOldestEmp()
        {
            // join employee and salary list on employee id based on monthly salary, and sort by age in descending order.
            var resultList = from emp in employeeList.Cast<Employee>()
                             join sal in salaryList.Cast<Salary>() on emp.EmployeeID equals sal.EmployeeID
                             where sal.Type == SalaryType.Monthly
                             orderby emp.Age descending
                             select new { emp.EmployeeID, emp.EmployeeFirstName, emp.EmployeeLastName, sal.Amount, emp.Age };

            Console.WriteLine("---------------------------------Task 2---------------------------------");
            // Console.WriteLine("####################### 2nd OLDEST EMPLOYEE #######################\n");

            Console.WriteLine("Employee ID\tEmployee Full Name\tSalary\t\tAge\n");
            var oldEmp = resultList.ElementAt(1); // get the 2nd oldest employee
            Console.WriteLine(oldEmp.EmployeeID + "\t\t" + oldEmp.EmployeeFirstName + " " + oldEmp.EmployeeLastName + "\t\t" + oldEmp.Amount + "\t\t" + oldEmp.Age);
        }

        public void CalculateMean()
        {
            // select all employees ID whose age is greater than 30.
            var idList = from emp in employeeList.Cast<Employee>() where emp.Age > 30 select new { emp.EmployeeID, emp.EmployeeLastName, emp.EmployeeFirstName };

            // join employee and salary list on employee id select all data which is important for our requirement.
            var resultList = from emp in employeeList.Cast<Employee>()
                             join sal in salaryList.Cast<Salary>() on emp.EmployeeID equals sal.EmployeeID
                             select new { emp.EmployeeID, emp.EmployeeFirstName, emp.EmployeeLastName, sal.Amount, sal.Type };

            Console.WriteLine("---------------------------------Task 3---------------------------------");
            Console.WriteLine("Employee ID\tEmployee Full Name\tMean Salary\n");
            foreach (var id in idList) // loop through each employee ID whose age is greater than 30.
            {
                double mean = 0;
                int count = 0;
                foreach (var empData in resultList) // loop through each employee data and calculate mean salary if employee ID matches.
                {
                    if (id.EmployeeID == empData.EmployeeID)
                    {
                        mean += empData.Amount;
                        count++;
                    }
                }
                mean = Math.Round(mean / count, 2); // calculate mean salary by dividing total salary by number of salaries.
                Console.WriteLine(id.EmployeeID + "\t\t" + id.EmployeeFirstName + " " + id.EmployeeLastName + "\t\t" + mean);
            }
            Console.WriteLine("------------------------------------------------------------------------");

        }

        public enum SalaryType
        {
            Monthly,
            Performance,
            Bonus
        }

        public class Employee
        {
            public int EmployeeID { get; set; }
            public string? EmployeeFirstName { get; set; }
            public string? EmployeeLastName { get; set; }
            public int Age { get; set; }
        }

        public class Salary
        {
            public int EmployeeID { get; set; }
            public int Amount { get; set; }
            public SalaryType Type { get; set; }
        }
    }
}