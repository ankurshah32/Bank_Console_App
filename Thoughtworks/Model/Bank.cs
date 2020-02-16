using System;
using System.Collections.Generic;
using System.Text;
using Thoughtworks.BusinessLogic.Interface;

namespace Thoughtworks.Model
{
    public class Bank: IModel
    {
        public List<Account> BankAccounts { get; private set; }
        public Bank()
        {
            BankAccounts = new List<Account>();
        }

        public int AddAccount(Account account)
        {
            BankAccounts.Add(account);
            return 0;
        }
        public Account GetAccountDetails(int accountNumber)
        {
            Account account = this.BankAccounts.Find(acc => acc.AccountNumber == accountNumber);
            return account;
        }
    }
}
