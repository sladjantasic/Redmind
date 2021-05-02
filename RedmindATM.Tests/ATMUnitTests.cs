using System;
using System.Collections.Generic;
using Xunit;

namespace RedmindATM.Tests
{
    public class ATMUnitTests
    {
        private ATMHandler atmHandler;
        private Dictionary<Bill, int> cash;
        
        public ATMUnitTests()
        {
            atmHandler = new ATMHandler();
            cash = atmHandler.availableCashForWithdrawal;
        }

        [Fact]
        public void ATM_Balance_Test()
        {
            cash[Bill.FiveHundreds] = 7;
            cash[Bill.Hundred] = 7;
            cash[Bill.Thousand] = 7;

            var atmBalance = atmHandler.GetATMBalance();

            Assert.Equal(11200, atmBalance);
        }

        [Fact]
        public void Sufficient_Funds_Test()
        {
            var largeTransactionAmount = 10_000;
            var smallTransactionAmount = 100;
            var exactTransactionAmount = 4000;

            var largeTransaction = atmHandler.CheckForSufficientBalance(largeTransactionAmount);
            var smallTransaction = atmHandler.CheckForSufficientBalance(smallTransactionAmount);
            var exactTransaction = atmHandler.CheckForSufficientBalance(exactTransactionAmount);

            Assert.True(smallTransaction);
            Assert.False(largeTransaction);
            Assert.True(exactTransaction);
        }

        [Fact]
        public void Withdrawal_Insufficient_Balance_Test()
        {
            var largeTransactionAmount = 10_000;
            var exactTransactionAmount = 4000;

            var largeTransaction = atmHandler.WithdrawCash(largeTransactionAmount);
            var exactTransaction = atmHandler.WithdrawCash(exactTransactionAmount);

            Assert.Equal(WithdrawalResult.InsufficientFunds, largeTransaction);
            Assert.NotEqual(WithdrawalResult.InsufficientFunds, exactTransaction);
        }

        [Fact]
        public void Withdrawal_Bills_Unavailable_Test()
        {
            cash[Bill.Hundred] = 1;
            var transactionAmount = 700;

            var transaction = atmHandler.WithdrawCash(transactionAmount);

            Assert.Equal(WithdrawalResult.NoSuitableBillsAvailable, transaction);
        }

        [Fact]
        public void Successful_Withdrawal_Test()
        {
            var transactionAmount = 3400;
            
            var transaction = atmHandler.WithdrawCash(transactionAmount);
            
            Assert.Equal(WithdrawalResult.Success, transaction);
        }

        [Fact]
        public void Successful_Maximum_Withdrawal_Test()
        {
            var exactTransactionAmount = 4000;

            var exactTransaction = atmHandler.WithdrawCash(exactTransactionAmount);

            Assert.Equal(WithdrawalResult.Success, exactTransaction);
        }
    }
}
