using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public static class DateHelper
    {
        public static List<DateTime> GetDatesInPeriod(DateTime startDate, DateTime endDate)
        {
            var result = new List<DateTime>();

            while (startDate < endDate)
            {
                result.Add(startDate);
                startDate = startDate.AddMonths(1);
            }

            result.OrderBy(x => x);

            return result;
        }
    }
}
