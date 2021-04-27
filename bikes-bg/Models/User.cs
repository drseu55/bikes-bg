using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace bikes_bg.Models
{
    public class User : IdentityUser
    {
        [Column("FIRST_NAME")]
        public string firstName { get; set; }
        [Column("LAST_NAME")]
        public string lastName { get; set; }
    }
}
