using BankingApplication.DataLayer.Contracts;
using BankingApplication.EFLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.EFLayer.Implementations
{
    public class AccountRepositoryEFImpl:IAccountRepository 
    {
        #region Private 
        private readonly db_bankingContext dbContext = null;
        private readonly AccountMinRepository accountMin;
        #endregion

        #region Public Methods

        public AccountRepositoryEFImpl(db_bankingContext dbContext)
        {
            this.dbContext = dbContext;
            this.accountMin = new AccountMinRepository(dbContext);
        }

        public string AddAccount(CommonLayer.Models.Account account)
        {
            string acno = string.Empty;
            if (account.AccountType.Equals("saving"))
            {
                if (account.Balance >= accountMin.MinSavings())
                {
                    var result = this.dbContext.GetSavingsSequence();
                    int? nextSequenceValue = result;
                    var acId = "SB00000" + nextSequenceValue.ToString();
                    acno = AddNewAccount(account,acId);
                }
            }

            else if (account.AccountType.Equals("corporate"))
            {
                if(account.Balance >= accountMin.MinCorporate())
                {
                    var result = this.dbContext.GetCorporateSequence();
                    int? nextSequenceValue = result;
                    var acId = "CO00000" + nextSequenceValue.ToString();
                    acno = AddNewAccount(account,acId);
                }
            }

            else
            {
                if(account.Balance >= accountMin.MinCurrent())
                {
                    var result = this.dbContext.GetCurrentSequence();
                    int? nextSequenceValue = result;
                    var acId = "CA00000" + nextSequenceValue.ToString();
                    acno = AddNewAccount(account,acId);
                }

            }

            return acno;
        }

        public bool DeleteAccount(string acNumber)
        {
            bool isDeleted = false;
            var accountToDelete = this.dbContext.Accounts.FirstOrDefault(d => d.AccountNumber.Equals(acNumber));
            if(accountToDelete != null)
            {
                accountToDelete.IsDeleted = true;
                this.dbContext.SaveChanges();
                isDeleted = true;
            }
            return isDeleted;
        }

        public CommonLayer.Models.Account GetAccount(string id,bool isCustomerId)
        {
            CommonLayer.Models.Account account = null;
            if (isCustomerId)
            {
                var accountDb = this.dbContext.Accounts.FirstOrDefault(a => a.CustomerId.Equals(id) 
                                                                       && a.IsDeleted == false);
                if (accountDb != null)
                {
                   account = GetAccount(accountDb); 
                }
            }
            else
            {
                var accountDb = this.dbContext.Accounts.FirstOrDefault(a => a.AccountNumber.Equals(id)
                                                                       && a.IsDeleted == false);
                if (accountDb != null)
                {
                    account = GetAccount(accountDb);
                }
            }
            return account;
        }

        public CommonLayer.Models.Account GetAccount(CommonLayer.Models.Account account)
        {
            CommonLayer.Models.Account accountExist = null;
            var accountDb = this.dbContext.Accounts.FirstOrDefault(a => a.CustomerId.Equals(account.CustomerId) 
                                                                    && a.AccountType.Equals(account.AccountType)
                                                                    && a.IsDeleted == false);
            if (accountDb != null)
            {
                accountExist.AccountNumber = accountDb.AccountNumber;
                accountExist.AccountType = accountDb.AccountType;
                accountExist.Tin = accountDb.Tin;
            }
            return accountExist;
        }

        public IEnumerable<CommonLayer.Models.Account> GetCustomerAccounts(string customerId)
        {
            var accountsDb = this.dbContext.Accounts;
            var result = (from account in accountsDb
                          where account.CustomerId.Contains(customerId) 
                          && account.IsDeleted == false && account.AccountType.Contains("current")
                          select new BankingApplication.CommonLayer.Models.Account
                          {
                              CustomerId = account.CustomerId,
                              Balance = account.Balance,
                              Doc = account.Doc,
                              Tin = account.Tin,
                              AccountType = account.AccountType,
                              Ifsc = account.Ifsc
                          }).ToList();
            return result;
        }

        public bool CurrentAccountValidation(string accountNo, string tin)
        {
            bool isValid = false;
            var currentAc = this.dbContext.Accounts.FirstOrDefault(c => c.AccountNumber.Equals(accountNo)
                                                                   && c.Tin.Equals(tin));
            if (currentAc != null)
            {
                isValid = true;
            }
            return isValid;
        }

        public bool UpdateAccount(CommonLayer.Models.Account account, bool isACType)
        {
            bool isUpdated = false;
            var accountDb = this.dbContext.Accounts.FirstOrDefault(a => a.AccountNumber.Equals(account.AccountNumber)
                                                                   && a.IsDeleted == false);
            if (accountDb != null)
            {
                if (isACType == false)
                {
                    accountDb.AccountNumber = account.AccountNumber;
                    accountDb.CustomerId = account.CustomerId;
                    accountDb.Balance = account.Balance;
                    accountDb.Doc = account.Doc;
                    accountDb.Tin = account.Tin;
                    accountDb.AccountType = account.AccountType;
                    accountDb.Ifsc = account.Ifsc;
                    accountDb.IsDeleted = false;

                    this.dbContext.SaveChanges();
                    isUpdated = true;
                }
                else
                {
                    accountDb.IsDeleted = true;
                    this.dbContext.SaveChanges();
                    if (!(String.IsNullOrEmpty(AddAccount(account))))
                    {
                        isUpdated = true;
                    }
                } 
            }
            return isUpdated;
        }

        #endregion

        #region Private Methods

        private string AddNewAccount(CommonLayer.Models.Account account,string acId)
        {
            var newAccount = new Account()
            {   
                AccountNumber = acId,
                CustomerId = account.CustomerId,
                Balance = account.Balance,
                Doc = account.Doc,
                Tin = account.Tin,
                AccountType = account.AccountType,
                Ifsc = account.Ifsc,
                IsDeleted = false
            };

            this.dbContext.Accounts.Add(newAccount);
            this.dbContext.SaveChanges();
            return newAccount.AccountNumber;
        }

        private CommonLayer.Models.Account GetAccount(Account accountDb)
        {
            var account = new CommonLayer.Models.Account()
            {
                AccountNumber = accountDb.AccountNumber,
                AccountType = accountDb.AccountType,
                Tin = accountDb.Tin,
                CustomerId = accountDb.CustomerId,
                Balance = accountDb.Balance
            };
            return account;
        }

        #endregion
    }
}
