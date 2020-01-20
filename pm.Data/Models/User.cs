using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pm.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [MinLength(10)]
        public string NationalCode { get; set; }

        [StringLength(11)]
        public string Mobile { get; set; }

        [StringLength(11)]
        public string Phone { get; set; }


        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string ClaimJwtId { get; set; }
        public ClaimJwt ClaimJwt { get; set; }


        //public virtual ICollection<Department> Departments { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }


        //readonly
        public string  FullName { get { return FirstName + " " + LastName; } }
    }
}
