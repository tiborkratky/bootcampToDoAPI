using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bootcamp.ToDoList.Backend.Entities.DTO
{
    public class UserDto
    {

        public string Username { get; set; }
        public string Password { get; set; }
    }
}