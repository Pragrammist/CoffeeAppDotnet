using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IHasherService
    {
        public string HashPassword(string password);

        public string GetHashForToken<TValue>(TValue value);
    }
}
