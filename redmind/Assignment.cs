using Microsoft.Extensions.Configuration;
using System;

namespace RedmindATM
{
    internal class Assignment
    {
        private readonly IConfiguration config;
        private readonly IATMHandler atmHandler;

        public Assignment(IConfiguration configuration, IATMHandler atmHandler)
        {
            config = configuration;
            this.atmHandler = atmHandler;
        }

        internal void Run()
        {
            PrintCurrentBalance();
            PrintWithdrawalAttemptAmount("Withdrawals:First");
            DoTransaction("Withdrawals:First");

            PrintCurrentBalance();
            PrintWithdrawalAttemptAmount("Withdrawals:Second");
            DoTransaction("Withdrawals:Second");

            PrintCurrentBalance();
            PrintWithdrawalAttemptAmount("Withdrawals:Third");
            DoTransaction("Withdrawals:Third");

            PrintCurrentBalance();
            PrintWithdrawalAttemptAmount("Withdrawals:Fourth");
            DoTransaction("Withdrawals:Fourth");

            PrintCurrentBalance();
            PrintWithdrawalAttemptAmount("Withdrawals:Fifth");
            DoTransaction("Withdrawals:Fifth");

            PrintCurrentBalance();
            PrintWithdrawalAttemptAmount("Withdrawals:Sixth");
            DoTransaction("Withdrawals:Sixth");

            PrintCurrentBalance();
            PrintWithdrawalAttemptAmount("Withdrawals:Seventh");
            DoTransaction("Withdrawals:Seventh");

        }

        internal void PrintCurrentBalance()
        {
            Console.WriteLine($"Current Balance is: {atmHandler.GetATMBalance()}");
        }

        internal void PrintWithdrawalAttemptAmount(string appsettingsValue)
        {
            Console.WriteLine($"Attempting to withdraw: {config.GetValueFromAppsettings(appsettingsValue)}");
        }

        internal void DoTransaction(string appsettingsValue)
        {
            var transactionResult = atmHandler.WithdrawCash(config.GetValueFromAppsettings(appsettingsValue));

            string something = transactionResult switch
            {
                WithdrawalResult.Success => "Withdrawal completed",
                WithdrawalResult.InsufficientFunds => "Insufficient funds",
                WithdrawalResult.NoSuitableBillsAvailable => "No suitable bills available",
                _ => "Something really wrong happened",
            };
            Console.WriteLine(something);
        }
    }
}