using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Dtos.BaseDtos
{
    public class BaseOutputDto
    {
        public int Id {get; set;} = default!;
        
        public DateTime CreatedDate { get; set; }

        public DateTime? LastUpdatedDate { get; set; } // пользователь сам имзенил

        public DateTime? LastChangedDate { get; set; } // модератор/админ

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
    
}