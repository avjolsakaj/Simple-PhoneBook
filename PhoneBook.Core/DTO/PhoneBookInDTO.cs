using System.Collections.Generic;

namespace PhoneBook.DTO.DTO
{
    public class PhoneBookInDTO
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool Deleted { get; set; }

        public List<NumberInfoInDTO> NumberInfo { get; set; }
    }
}
