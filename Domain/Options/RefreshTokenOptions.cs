using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Options
{
    public class RefreshTokenOptions
    {
        public int RefreshTokenExpired { get; set; }

        public DateTimeOffset GetDateTimeForRefreshTokenExpiredTime() => DateTimeOffset.UtcNow.AddDays(RefreshTokenExpired);

        public long GetRandomValue() => DateTime.UtcNow.Ticks;
    }
}
