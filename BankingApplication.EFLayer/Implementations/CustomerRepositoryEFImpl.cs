using BankingApplication.DataLayer.Contracts;
using BankingApplication.EFLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.EFLayer.Implementations
{
    public class CustomerRepositoryEFImpl: ICustomerRepository
    {
        private readonly db_bankingContext dbContext = null;
        public CustomerRepositoryEFImpl(db_bankingContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string AddCustomer(BankingApplication.CommonLayer.Models.Customer customer)
        {
            var customerDb = new Customer()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Dob = customer.Dob,
                EmailId = customer.EmailId,
                Gender = customer.Gender,
                MobileNumber = customer.MobileNumber,
                City = customer.City,
                State = customer.State,
                Pincode = customer.Pincode,
                IsDeleted = customer.IsDeleted
            };

            this.dbContext.Customers.Add(customerDb);
            this.dbContext.SaveChanges();
            var managerCustomer = new ManagerCustomerRelation()
            {
                ManagerId = customer.ManagerId,
                CustomerId = customerDb.CustomerId
            };
            this.dbContext.ManagerCustomerRelations.Add(managerCustomer);
            this.dbContext.SaveChanges();

            return customerDb.CustomerId;
        }

        public bool DeleteCustomer(string cId)
        {
            bool isDeleted = false;
            var customerDb = this.dbContext.Customers.FirstOrDefault(c => c.CustomerId.Equals(cId));
            try
            {
                if (customerDb != null)
                {
                    customerDb.IsDeleted = true;
                    this.dbContext.SaveChanges();
                    isDeleted = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return isDeleted;
        }

        public BankingApplication.CommonLayer.Models.Customer GetCustomer(string cId,string managerID,bool isEmail)
        {
            BankingApplication.CommonLayer.Models.Customer customer = null;
            bool validAccess = ManagerCustomerValidation(cId,managerID);
            try
            {
                if (!isEmail)
                {
                    var customerDb = this.dbContext.Customers.FirstOrDefault(c => c.CustomerId.Equals(cId));
                    
                    if (customerDb != null && validAccess)
                    {
                        customer = new BankingApplication.CommonLayer.Models.Customer()
                        {
                            FirstName = customerDb.FirstName,
                            LastName = customerDb.LastName,
                            Dob = customerDb.Dob,
                            EmailId = customerDb.EmailId,
                            ManagerId = customerDb.ManagerId
                        };
                    }
                }
                else
                {
                    var customerDb = this.dbContext.Customers.FirstOrDefault(c => c.EmailId.Equals(cId));
                    if (customerDb != null && validAccess)
                    {
                        customer = new BankingApplication.CommonLayer.Models.Customer()
                        {
                            FirstName = customerDb.FirstName,
                            LastName = customerDb.LastName,
                            Dob = customerDb.Dob,
                            EmailId = customerDb.EmailId,
                            ManagerId = customerDb.ManagerId
                        };
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return customer;
        }

        public IEnumerable<BankingApplication.CommonLayer.Models.Customer> GetCustomers()
        {
            var customersDb = this.dbContext.Customers.ToList();
            var result = (from customer in customersDb
                          where customer.IsDeleted == false
                          select new BankingApplication.CommonLayer.Models.Customer
                          {
                              FirstName = customer.FirstName,
                              LastName = customer.LastName,
                              Dob = customer.Dob,
                              EmailId = customer.EmailId,
                              ManagerId = customer.ManagerId,
                              Gender = customer.Gender,
                              MobileNumber = customer.MobileNumber,
                              City = customer.City,
                              State = customer.State,
                              Pincode = customer.Pincode,
                              IsDeleted = customer.IsDeleted
                          }).ToList();
            return result;
        }

        public bool UpdateCustomer(CommonLayer.Models.Customer customer)
        {
            bool isUpdated = false;
            var customerDb = this.dbContext.Customers.FirstOrDefault(c => c.CustomerId.Equals(customer.CustomerId)
                                                                    && c.IsDeleted == false);
            if (customerDb != null)
            {
                customerDb.FirstName = customer.FirstName;
                customerDb.LastName = customer.LastName;
                customerDb.Dob = customer.Dob;
                customerDb.EmailId = customer.EmailId;
                customerDb.Gender = customer.Gender;
                customerDb.MobileNumber = customer.MobileNumber;
                customerDb.City = customer.City;
                customerDb.State = customer.State;

                this.dbContext.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }

        private bool ManagerCustomerValidation(string cId, string managerID)
        {
            var isValid = false;
            var managerCustomerR = this.dbContext.ManagerCustomerRelations.FirstOrDefault(m => m.ManagerId.Equals(managerID)
                                                                                    && m.CustomerId.Equals(cId));
            if(managerCustomerR != null)
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
