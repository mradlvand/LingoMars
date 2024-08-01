﻿using Model.General;

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
        public Category ApplicationType { get; set; }
    }

    public class RegisterDtoResponce
    {
        public string Token { get; set; }
    }

}
