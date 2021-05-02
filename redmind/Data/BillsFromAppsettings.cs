using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedmindATM
{
    class BillsFromAppsettings : IBillsForATM
    {
        public Dictionary<Bill, int> AvailableBills { get; set; }

        public BillsFromAppsettings(IConfiguration config)
        {
            AvailableBills = new Dictionary<Bill, int>()
            {
                { Bill.Thousand, config.GetValueFromAppsettings("ATM:Bills:Thousand") },
                { Bill.FiveHundreds, config.GetValueFromAppsettings("ATM:Bills:FiveHundred") },
                { Bill.Hundred, config.GetValueFromAppsettings("ATM:Bills:Hundred") },
            };
        }
    }
}
