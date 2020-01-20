using System;
using System.Collections.Generic;
using System.Text;

namespace pm.Data.Models
{
    public class ClaimJwt
    {
        public ClaimJwt()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Jwt { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
