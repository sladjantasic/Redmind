using System;
using System.Collections.Generic;

namespace RedmindATM
{
    class ATM : IATM
    {
        public Dictionary<Bill, int> AvailableCash { get; set; }

        public ATM(IBillsForATM bills)
        {
            AvailableCash = bills.AvailableBills;
        }
        
    }
}
