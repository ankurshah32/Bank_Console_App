using System;
using System.Collections.Generic;
using System.Text;
using Thoughtworks.BusinessLogic.Interface;
using Thoughtworks.Model;

namespace Thoughtworks.BusinessLogic
{
    public class BankOperation : IBankOperation
    {
        private IModel _bank;
        private int AccountNumnber { get; set; } = 1001;
        public BankOperation(IModel bank)
        {
            _bank = bank;//new Bank(new List<Account>());
        }
        public int CreateAccount(string name)
        {
            Account account = new Account();
            lock (this)
            {
                account.CustomerName = name;
                account.AccountNumber = ++AccountNumnber;
                account.TranscationList = new List<Transcation>();
                this._bank.AddAccount(account);
            }
            return account.AccountNumber;
        }

        public double Deposit(int AcountId, double val)
        {
            var account = this._bank.GetAccountDetails(AcountId);
            int id = 0;
            if(account.TranscationList.Count > 0)
            {
                id = account.TranscationList[account.TranscationList.Count - 1].TranscationId;
            }
            Transcation tran = new Transcation();
            tran.TranscationId = ++id;
            tran.Withdrawl = 0;
            tran.Deposit = val;
            account.TranscationList.Add(tran);
            return val;

        }

        public Account GetAccount(int accountId)
        {
            return this._bank.GetAccountDetails(accountId);
        }

        public bool Login(int accountId)
        {
            return this._bank.GetAccountDetails(accountId) != null ? true: false ;
        }

        public double GetBalance(int AcountId)
        {
            var acc = this._bank.GetAccountDetails(AcountId);
            double total = 0;
            acc.TranscationList.ForEach(tran =>
            {
                if (tran.Deposit > 0)
                    total += tran.Deposit;
                else if (tran.Withdrawl > 0)
                    total -= tran.Withdrawl;
            });
            return total;
        }

        public List<Transcation> TranscationList(int AcountId)
        {
            return this._bank.GetAccountDetails(AcountId).TranscationList;
        }

        public double Withdrawl(int AcountId, double val)
        {
            var account = this._bank.GetAccountDetails(AcountId);
            int id = 0;
            if (account.TranscationList.Count > 0)
            {
                id = account.TranscationList[account.TranscationList.Count - 1].TranscationId;
            }
            Transcation tran = new Transcation();
            tran.TranscationId = ++id;
            tran.Deposit = 0;
            tran.Withdrawl = val;
            account.TranscationList.Add(tran);
            return val;
        }
    }
}
