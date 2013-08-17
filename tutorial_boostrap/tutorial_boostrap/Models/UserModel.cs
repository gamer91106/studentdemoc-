using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
namespace tutorial_boostrap.Models
{
    public class UserModel
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50,MinimumLength=6)]
        public string Password { get; set; }
    }
}