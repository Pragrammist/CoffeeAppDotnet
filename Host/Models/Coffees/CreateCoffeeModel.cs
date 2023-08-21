using Host.Infrastructure.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using static Host.Infrastructure.Consts.CoffeeModelsHelpersAndConsts;

namespace Host.Models.Coffees
{
    public class CreateCoffeeModel
    {
        [StringLength(MAX_LENGTH_NAME_CONST, MinimumLength = MIN_LENGTH_NAME_CONST, ErrorMessage = MESSAGE_NAME_NOT_VALID_LENGTH)]
        public string Name { get; set; } = null!;


        [StringLength(MAX_LENGTH_NOTE_CONST, MinimumLength = MIN_LENGTH_NOTE_CONST, ErrorMessage = MESSAGE_NOTE_NOT_VALID_LENGTH)]
        public string Note { get; set; } = null!;


        [Photo]
        public IEnumerable<IFormFile> Photos { get; set; } = null!;
    }
}
