using System;
using System.Collections.Generic;

#nullable disable

namespace abcApi.Models
{
    public partial class TblUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
        public string Mobile { get; set; }
    }
}
