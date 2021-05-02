using System.Collections.Generic;

namespace RedmindATM
{
    interface IBillsForATM
    {
        Dictionary<Bill, int> AvailableBills { get; set; }
    }
}