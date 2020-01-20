using pm.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pm.Dto
{
    public class UpdateUserDto
    {
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


        public ICollection<UserRole> UserRoles { get; set; }

    }
}
