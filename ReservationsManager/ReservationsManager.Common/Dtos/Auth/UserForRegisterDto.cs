﻿namespace ReservationsManager.Common.Dtos.Auth
{
    public class UserForRegisterDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
