﻿using Azure.Identity;

namespace ExamServer.EntityFramework.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsStudent { get; set; }

    }
}
