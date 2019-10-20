using System.ComponentModel.DataAnnotations;

namespace PhoneBook.DTO.DTO
{
    public class NumberInfoInDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }
        
        [Required]
        public int TypeId { get; set; }
    }
}
