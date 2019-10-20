using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.DTO.DTO
{
    public class PhoneBookInDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public bool Deleted { get; set; }

        public List<NumberInfoInDTO> NumberInfo { get; set; }
    }
}
