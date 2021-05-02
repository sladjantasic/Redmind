using System;
using System.Collections.Generic;
using System.Text;

namespace RedmindATM
{
    //This class only exists because I don't yet know how to use mocking libraries for testing
    class BillsForTestingPurposes : IBillsForATM
    {
        public Dictionary<Bill, int> AvailableBills { get; set; }

        public BillsForTestingPurposes()
        {
            AvailableBills = new Dictionary<Bill, int>()
            {
                { Bill.Thousand, 2 },
                { Bill.FiveHundreds, 3 },
                { Bill.Hundred, 5 },
            };
        }
    }
}
