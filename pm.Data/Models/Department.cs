using System;
using System.Collections.Generic;
using System.Text;

namespace pm.Data.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<User> User { get; set; }

    }
}
