using System;
using System.Collections.Generic;
using System.Text;
using Thoughtworks.Model;

namespace Thoughtworks.BusinessLogic.Interface
{
    public interface IModel
    {
        List<Account> BankAccounts { get; }
        int AddAccount(Account account);
         Account GetAccountDetails(int accountNumber);
    }
}
