using BankingApplication.DataLayer.Contracts;
using BankingApplication.EFLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.EFLayer.Implementations
{
    public class ManagerRepositoryEFImpl: IManagerRepository
    {
        private readonly db_bankingContext dbContext;

        public ManagerRepositoryEFImpl(db_bankingContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string AddEmployee(BankingApplication.CommonLayer.Models.Manager manager)
        {
            try
            {
                var employeeDb = new Models.Manager()
                {
                    ManagerId = manager.ManagerId,
                    FirstName = manager.FirstName,
                    LastName = manager.LastName,
                    Gender = manager.Gender,
                    Dob = manager.Dob,
                    EmailId = manager.EmailId,
                    ManagerPassword = manager.ManagerPassword,
                    MobileNumber = manager.MobileNumber
                };
                this.dbContext.Managers.Add(employeeDb);
                this.dbContext.SaveChanges();
                return employeeDb.ManagerId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        BankingApplication.CommonLayer.Models.Manager IManagerRepository.GetManager(string mId)
        {
            try
            {
                var employeeDb = this.dbContext.Managers.SingleOrDefault(x => x.EmailId.Contains(mId));
                BankingApplication.CommonLayer.Models.Manager employee = null;

                if (employeeDb != null)
                {
                    employee = new BankingApplication.CommonLayer.Models.Manager()
                    {
                        ManagerId = employeeDb.ManagerId,
                        FirstName = employeeDb.FirstName,
                        EmailId = employeeDb.EmailId,
                        ManagerPassword = employeeDb.ManagerPassword
                    };
                }
                return employee;
            }
            catch (Exception)
            {
                throw;
            }

        }

        IEnumerable<BankingApplication.CommonLayer.Models.Manager> IManagerRepository.GetManagers()
        {
            IEnumerable<BankingApplication.CommonLayer.Models.Manager> employees = null;
            try
            {
                var employeesDb = dbContext.Managers;
                if (employeesDb != null)
                {
                    employees = from manager in employeesDb
                                select new BankingApplication.CommonLayer.Models.Manager
                                {
                                    ManagerId = manager.ManagerId,
                                    FirstName = manager.FirstName,
                                    LastName = manager.LastName,
                                    Gender = manager.Gender,
                                    Dob = manager.Dob.Value,
                                    EmailId = manager.EmailId,
                                    ManagerPassword = manager.ManagerPassword
                                };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return employees;
        }
    }
}
