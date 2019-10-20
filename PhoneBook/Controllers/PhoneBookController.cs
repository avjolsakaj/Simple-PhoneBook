using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.BLL.Interface;
using PhoneBook.DTO.DTO;

namespace PhoneBook.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookService _phoneBookService;

        public PhoneBookController(IPhoneBookService phoneBookService)
        {
            _phoneBookService = phoneBookService;
        }

        /// <summary>
        /// Get all Phone Books ordered
        /// </summary>
        /// <param name="orderByFirstName"> Firstname or Lastname </param>
        /// <param name="asc"> True for asc and false for desc </param>
        /// <returns>List of phone books</returns>
        // GET api/values
        [HttpGet]
        public ActionResult<List<PhoneBookOutDTO>> Get(bool orderByFirstName = true, bool asc = true)
        {
            var result = _phoneBookService.GetAllOrderedBy(orderByFirstName, asc).ToList();

            return result;
        }

        /// <summary>
        /// Get all Phone Books types
        /// </summary>
        /// <returns>List of phone books types</returns>
        // GET api/values
        [HttpGet("GetTypes")]
        public ActionResult<IEnumerable<TypesOutDTO>> GetTypes()
        {
            var result = _phoneBookService.GetTypes();

            return result;
        }

        /// <summary>
        /// Get Phone Book by id
        /// </summary>
        /// <param name="id"> Specified id for phone book </param>
        /// <returns> Phone Book </returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<PhoneBookOutDTO> Get(int id)
        {
            var result = _phoneBookService.Get(id);

            return result;
        }

        /// <summary>
        /// Create Phone Book
        /// </summary>
        /// <param name="phoneBook"> Phone Book Entity </param>
        /// <returns> Created Phone Book </returns>
        // POST api/values
        [HttpPost]
        public PhoneBookOutDTO Post([FromBody] PhoneBookInDTO phoneBook)
        {
            var result = _phoneBookService.Post(phoneBook);

            return result;
        }
        
        /// <summary>
        /// Add new number in Phone Book
        /// </summary>
        /// <param name="phoneBook"> Phone Book Entity </param>
        /// <returns> Created Phone Book </returns>
        // POST api/values
        [HttpPost("AddNumber")]
        public PhoneBookOutDTO AddNumber([FromBody] PhoneBookInDTO phoneBook)
        {
            var result = _phoneBookService.AddNumber(phoneBook);

            return result;
        }

        /// <summary>
        /// Update Phone book
        /// </summary>
        /// <param name="id"> Phone Book id </param>
        /// <param name="phoneBook"> Phone Book Entity </param>
        /// <returns> Update phone book </returns>

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<PhoneBookOutDTO> Put(int id, [FromBody] PhoneBookInDTO phoneBook)
        {
            if (id != phoneBook.Id)
            {
                return new BadRequestObjectResult("Id is not right");
            }

            var result = _phoneBookService.Put(phoneBook);

            return result;
        }

        /// <summary>
        /// Delete Phone Book
        /// </summary>
        /// <param name="id"> Phone Book Id </param>
        /// <returns> True if deleted </returns>
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = _phoneBookService.Delete(id);

            return result;
        }
    }
}
