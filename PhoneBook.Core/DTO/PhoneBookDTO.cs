using System.Collections.Generic;

namespace PhoneBook.DTO.DTO
{
    public class PhoneBookOutDTO
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public List<NumberInfoOutDTO> NumberInfo { get; set; }
    }
}
