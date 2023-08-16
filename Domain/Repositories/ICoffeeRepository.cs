using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICoffeeRepository
    {
        public void CreateCoffee();

        public void GetCoffee();

        public void GetCoffees();

        public void UpdateCoffee();
    }
}