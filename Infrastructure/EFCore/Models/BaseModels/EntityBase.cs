using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Models
{
    [NotMapped]
    public class EntityBase
    {
        public int Id {get; set;} = default!;
        
        public DateTime CreatedDate { get; set; }

        public DateTime? LastUpdatedDate { get; set; } // пользователь сам имзенил

        public DateTime? LastChangedDate { get; set; } // модератор/админ

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}