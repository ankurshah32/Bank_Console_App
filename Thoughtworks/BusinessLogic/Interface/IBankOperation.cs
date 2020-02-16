using System;
using System.Collections.Generic;
using System.Text;
using Thoughtworks.Model;

namespace Thoughtworks.BusinessLogic.Interface
{
    public interface IBankOperation
    {
        bool Login(int accountId);
        Account GetAccount(int accountId);
        int CreateAccount(string name);
        double GetBalance(int AcountId);
        double Deposit(int AcountId, double val);
        double Withdrawl(int AcountId, double val);
        List<Transcation> TranscationList(int AcountId);
    }
}
