using System;
using System.Collections.Generic;
using System.Text;

namespace pm.Data.Models
{
    public class Role
    {
        public Role()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
