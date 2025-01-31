using System.ComponentModel.DataAnnotations;

namespace Restaurant_API.Model.DTO.Auth
{
    public class UpdateRequestDto
    {
        public string? Username { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
