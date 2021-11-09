using Microsoft.Extensions.Configuration;
using System;

namespace RedmindATM
{
    internal class Assignment
    {
        private readonly IConfiguration config;
        private readonly IATMHandler atmHandler;
        private readonly string[] withdrawals = new[] {"Withdrawals:First", "Withdrawals:Second", "Withdrawals:Third", "Withdrawals:Fourth",
                                                       "Withdrawals:Fifth", "Withdrawals:Sixth", "Withdrawals:Seventh"};

        public Assignment(IConfiguration configuration, IATMHandler atmHandler)
        {
            config = configuration;
            this.atmHandler = atmHandler;
        }

        internal void Run()
        {
            foreach (var withdrawal in withdrawals)
            {
                PrintCurrentBalance();
                PrintWithdrawalAttemptAmount(withdrawal);
                DoTransaction(withdrawal);
            }
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