using Model.General;

namespace Presentation.Dtos
{
    public class UserDto
    {
    }
    public class LoginDto
    {
        public string PhoneNumber { get; set; }
    }

    public class RegisterDto
    {
        public string PhoneNumber { get; set; }
        public Category Category { get; set; }
    }

    public class RegisterDtoResponce
    {
    }

}
