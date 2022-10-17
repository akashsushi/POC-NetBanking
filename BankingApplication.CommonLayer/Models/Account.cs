using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.CommonLayer.Models
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public string CustomerId { get; set; }
        public string AccountType { get; set; }
        public double Balance { get; set; }
        public DateTime Doc { get; set; }
        public string Tin { get; set; }
        public string Ifsc { get; set; }
        public bool IsDeleted { get; set; }

        //private List<CurrentAccount> currentAccounts = null;

        //private List<SavingsAccount> savingsAccounts = null;

        //private List<CorporateAccount> corporateAccounts = null;
        
        ///// <summary>
        ///// Method to add Current Account
        ///// </summary>
        ///// <param name="currentAccount"></param>
        //public void AddCurrentAccount(CurrentAccount currentAccount)
        //{
        //    this.currentAccounts.Add(currentAccount);
        //}

        ///// <summary>
        ///// Method to get Current Account
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<CurrentAccount> GetCurrentAccounts()
        //{
        //    return currentAccounts;
        //}

        ///// <summary>
        ///// Method to add Savings Account
        ///// </summary>
        ///// <param name="currentAccount"></param>
        //public void AddSavingsAccount(SavingsAccount savingsAccount)
        //{
        //    this.savingsAccounts.Add(savingsAccount);
        //}

        ///// <summary>
        ///// Method to get Savings Account
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<SavingsAccount> GetSavingstAccounts()
        //{
        //    return savingsAccounts;
        //}

        ///// <summary>
        ///// Method to add Corporate Account
        ///// </summary>
        ///// <param name="currentAccount"></param>
        //public void AddCorporateAccount(CorporateAccount corporateAccount)
        //{
        //    this.corporateAccounts.Add(corporateAccount);
        //}

        ///// <summary>
        ///// Method to get Corporate Account
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<CorporateAccount> GetCorporateAccounts()
        //{
        //    return corporateAccounts;
        //}

    }
}
