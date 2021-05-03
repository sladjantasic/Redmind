using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedmindATM
{
    
    class ATMHandler : IATMHandler
    {
        internal readonly Dictionary<Bill, int> availableCashForWithdrawal;

        public ATMHandler(IATM atm)
        {
            availableCashForWithdrawal = atm.AvailableCash;
        }

        public WithdrawalResult WithdrawCash(int amount)
        {
            if (!CheckForSufficientBalance(amount)) return WithdrawalResult.InsufficientFunds;

            availableCashForWithdrawal.OrderByDescending(b => b.Key);

            int billCount;
            var withdrawnBills = new Dictionary<Bill, int>();
            foreach (var item in availableCashForWithdrawal)
            {
                if ((int)item.Key > amount || availableCashForWithdrawal[item.Key] == 0) continue;

                int amountOfSameBillsWithdrawn;

                billCount = amount / (int)item.Key;
                if (billCount > availableCashForWithdrawal[item.Key])
                {
                    amountOfSameBillsWithdrawn = availableCashForWithdrawal[item.Key];
                    amount -= amountOfSameBillsWithdrawn * (int)item.Key;
                }
                else
                {
                    amountOfSameBillsWithdrawn = billCount;
                    amount %= (int)item.Key;
                }
                withdrawnBills.Add(item.Key, amountOfSameBillsWithdrawn);
                if (amount == 0) break;
            }

            if (amount != 0) return WithdrawalResult.NoSuitableBillsAvailable;

            foreach (var item in withdrawnBills)
            {
                availableCashForWithdrawal[item.Key] -= item.Value;
            }

            return WithdrawalResult.Success;

        }

        public bool CheckForSufficientBalance(int transaction)
        {
            return GetATMBalance() >= transaction;
        }

        public int GetATMBalance()
        {
            var hundreds = availableCashForWithdrawal[Bill.Hundred] * (int)Bill.Hundred;
            var fiveHundreds = availableCashForWithdrawal[Bill.FiveHundreds] * (int)Bill.FiveHundreds;
            var thousands = availableCashForWithdrawal[Bill.Thousand] * (int)Bill.Thousand;

            var totalAmount = hundreds + thousands + fiveHundreds;

            return totalAmount;
        }

    }
}
