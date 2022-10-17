using BankingApplication.BusinessLayer.Contracts;
using BankingApplication.BusinessLayer.Implementation;
using BankingApplication.CommonLayer.Models;
using BankingApplication.DataLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.ConsoleApp
{
    public class EmployeeMain
    {
        public void Main()
        {
            db_bankingContext dbContext = new db_bankingContext();
            IManagerRepository managerRepository = new ManagerRepositoryEFImpl(dbContext);

            IEmployeeManager employeeManager = new EmployeeManager(managerRepository);

            //AddManager(employeeManager);
            GetManager(employeeManager);
        }

        private void GetManager(IEmployeeManager employeeManager)
        {
            var manager = employeeManager.GetManager("akashsushi@gmail.com");
            Console.WriteLine(manager.EmailId);
        }

        private void AddManager(IEmployeeManager employeeManager)
        {
            CommonLayer.Models.Manager manager = new CommonLayer.Models.Manager()
            {
                FirstName = "Rahul",
                LastName = "J",
                Dob = new DateTime(1999,05,23),
                EmailId = "rahul@gmail.com",
                Gender = "Male",
                ManagerPassword = "Pass@123",
                MobileNumber = "9598231277"
            };
             var id = employeeManager.AddEmployee(manager);

            if(id != null)
            {
                Console.WriteLine("Passed");
            }
            else
            {
                Console.WriteLine("Failed");
            }
        }
    }
}
