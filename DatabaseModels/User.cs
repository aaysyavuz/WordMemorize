using System;
using System.Collections.Generic;

#nullable disable

namespace WordMemorize.DatabaseModels
{
    public partial class User
    {
        public uint Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
    }
}
