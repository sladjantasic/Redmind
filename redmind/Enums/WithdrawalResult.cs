using System;
using System.Collections.Generic;
using System.Text;

namespace RedmindATM
{
    public enum WithdrawalResult
    {
        Success,
        InsufficientFunds,
        NoSuitableBillsAvailable
    }
}
