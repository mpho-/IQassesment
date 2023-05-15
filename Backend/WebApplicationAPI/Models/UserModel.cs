﻿using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? ProfilePicture { get; set; }
        public string Role { get; set; }
        public int[] CurrentProjects { get; set; }
        public int[] ReportingLine { get; set; }
    }
    
}
