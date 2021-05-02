using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedmindATM
{
    static class Extensions
    {
        public static int GetValueFromAppsettings(this IConfiguration config, string name)
        {
            var section = config.GetSection(name).Value;
            return int.TryParse(section, out int result) ? result : 0;
        }
    }
}
