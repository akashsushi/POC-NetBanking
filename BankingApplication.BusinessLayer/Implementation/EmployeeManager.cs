using BankingApplication.BusinessLayer.Contracts;
using BankingApplication.CommonLayer.Models;
using BankingApplication.DataLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.BusinessLayer.Implementation
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IManagerRepository managerRepository;

        public EmployeeManager(IManagerRepository managerRepository)
        {
            this.managerRepository = managerRepository;
        }

        public string AddEmployee(Manager manager)
        => this.managerRepository.AddEmployee(manager);

        public Manager GetManager(string mId)
        => this.managerRepository.GetManager(mId);

        public IEnumerable<Manager> GetManagers()
        => this.managerRepository.GetManagers();
    }
}
