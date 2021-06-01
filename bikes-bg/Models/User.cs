using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace bikes_bg.Models
{
    [Table("USERS")]
    public class User : IdentityUser
    {
        [Column("FIRST_NAME")]
        [DisplayName("First Name")]
        public string firstName { get; set; }
        [Column("LAST_NAME")]
        [DisplayName("Last Name")]
        public string lastName { get; set; }
    }
}
