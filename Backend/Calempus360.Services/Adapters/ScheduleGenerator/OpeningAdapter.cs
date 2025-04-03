using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calempus360.Services.Adapters.ScheduleGenerator
{
    public static class OpeningAdapter
    {
        public static (string site, string dayOfWeek, (int hour, int) startEndHour) Adapt((string site, string dayOfWeek, (int hour, int) startEndHour) opening)
        {
            return opening;
        }
    }
}