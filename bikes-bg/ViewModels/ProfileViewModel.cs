using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace bikes_bg.ViewModels
{
    public class ProfileViewModel
    {
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("First Name")]
        public string selectedFirstName { get; set; }

        [DisplayName("Last Name")]
        public string selectedLastName { get; set; }

        [DisplayName("Phone Number")]
        public string selectedPhoneNumber { get; set; }
    }
}
