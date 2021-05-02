using System.Collections.Generic;

namespace RedmindATM
{
    interface IATM
    {
        Dictionary<Bill, int> AvailableCash { get; set; }
    }
}