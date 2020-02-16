using System;
using System.Collections.Generic;
using System.Text;
using Thoughtworks.BusinessLogic.Interface;

namespace Thoughtworks
{
    public class BankUI
    {
        private int UserSession = -1;
        private IBankOperation bankOperation;
        public BankUI(IBankOperation bankOperation){
            this.bankOperation = bankOperation;
        }

        public void WelcomeScreen()
        {
            Console.WriteLine("Welcome to ABC bank");
            Console.WriteLine("Please select the below options");
            Console.WriteLine("1. Open an Account.");
            Console.WriteLine("2. Login to bank account.");
            Console.WriteLine("3. Exit from app");
            Console.Write("Option Selected : ");
            try
            {
                int OptionSelected = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (OptionSelected)
                {
                    case 1:OpenAccountScreen();
                        break;
                    case 2:LoginScreen();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Please select right option");
                        WelcomeScreen();
                        break;
                }
                
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Please select correct number option");
                WelcomeScreen();
            }
        }
        void LoginScreen()
        {
            Console.WriteLine("Please enter Account Number");
            Console.Write("Account Number : ");
            try
            {
                int accountNumber = int.Parse(Console.ReadLine());
                if (bankOperation.Login(accountNumber))
                {
                    UserSession = accountNumber;
                    Console.Clear();
                    AfterSuccessFullLoginScreen();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Account number not found");
                    LoginScreen();
                }
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Please select correct option");
                LoginScreen();
            }
        }
        private void AfterSuccessFullLoginScreen()
        {
            
            Console.WriteLine("Please select the below options");
            Console.WriteLine("1. Balance Check.");
            Console.WriteLine("2. Deposit.");
            Console.WriteLine("3. Withdrawl.");
            Console.WriteLine("4. List all Transcation.");
            Console.WriteLine("5. Logout");
            try
            {
                int OptionSelected = int.Parse(Console.ReadLine());
                 ShowOptionsAvailable(OptionSelected);
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Please select correct option");
                AfterSuccessFullLoginScreen();
            }
        }
        private void ShowOptionsAvailable(Int32 option)
        {
            switch (option)
            {
                case 1:// Check Balance
                    ShowBalance();
                    break;
                case 2:// Deposit
                    Console.Clear();
                    Deposit();
                    break;
                case 3:// Withdrawl
                    Console.Clear();
                    Withdrawl();
                    break;
                case 4:// List all transcation
                    TranscationList();
                    break;
                case 5:// logout
                    UserSession = -1;
                    Console.Clear();
                    WelcomeScreen();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please enter valid option ");
                    AfterSuccessFullLoginScreen();
                    break;
            }
        }

        private void OpenAccountScreen()
        {
            Console.Clear();
            Console.WriteLine("Please Enter details");
            Console.Write("Name : ");
            var AccNum = bankOperation.CreateAccount(Console.ReadLine());
            Console.WriteLine("Your Account is Created Successfully");
            Console.WriteLine($"Your Account number is '{AccNum}'");
            UserSession = AccNum;
            Console.Read();
            Console.Clear();
            AfterSuccessFullLoginScreen();

        }
        private void ShowBalance()
        {
            var balance = bankOperation.GetBalance(UserSession);
            Console.WriteLine($"Account No. {UserSession} balance is {balance}");
            Console.ReadKey();
            Console.Clear();
            AfterSuccessFullLoginScreen();
        }
        private void Deposit()
        {
            Console.WriteLine("Please enter amount to deposit");
            Console.Write("Amount :");
            try
            {
                var amount = double.Parse(Console.ReadLine());
                var accountBalance = bankOperation.Deposit(UserSession, amount);
                Console.WriteLine($"You have deposit {amount} in this Account No. {UserSession}");
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Please enter correct amount in number");
                Deposit();
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Invalid Transaction");
                AfterSuccessFullLoginScreen();
            }
            Console.ReadKey();
            Console.Clear();
            AfterSuccessFullLoginScreen();
        }

        private void Withdrawl()
        {
            Console.WriteLine("Please enter amount to Withdraw");
            Console.Write("Amount :");
            try
            {
                var amount = double.Parse(Console.ReadLine());
                var accountBalance = bankOperation.GetBalance(UserSession);
                if(accountBalance >= amount && amount > 0)
                { 
                    bankOperation.Withdrawl(UserSession, amount);
                    Console.WriteLine($"You have Withdrawl {amount} from this Account No. {UserSession}");
                }
                else
                {
                    Console.WriteLine($"Your balance is less than Withdrawl ");
                }
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Please enter correct amount in number");
                Withdrawl();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Transaction");
                AfterSuccessFullLoginScreen();
            }
            Console.ReadKey();
            Console.Clear();
            AfterSuccessFullLoginScreen();
        }
        private void TranscationList()
        {
            var account = bankOperation.GetAccount(UserSession);
            Console.WriteLine($"Account Name {account.CustomerName}");
            Console.WriteLine($"Account Number {account.AccountNumber}");
            account.TranscationList.ForEach(transcation =>
            {
                Console.Write($"Transcation Id {transcation.TranscationId} ");
                if(transcation.Deposit > 0)
                {
                    Console.WriteLine($"Deposit {transcation.Deposit} ");
                } else if(transcation.Withdrawl > 0)
                {
                    Console.WriteLine($"Withdrawl {transcation.Withdrawl} ");
                }
            });
            Console.Read();
            Console.Clear();
            AfterSuccessFullLoginScreen();
        }

    }
}
