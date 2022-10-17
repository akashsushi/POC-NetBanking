using BankingApplication.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.DataLayer.Contracts
{
    public interface IManagerRepository
    {
        string AddEmployee(Manager manager);

        Manager GetManager(string mId);

        IEnumerable<Manager> GetManagers();

    }
}
