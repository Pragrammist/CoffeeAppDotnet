using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Models
{
    public class RefreshToken : EntityBase
    {
        public string Token { get; set; } = default!;
        public DateTimeOffset Expires { get; set; }


        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}