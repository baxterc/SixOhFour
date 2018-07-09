using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SixOhFour.Models
{
    public class ApplicationUser : IdentityUser
    {
        private string DisplayName { get; set; }
    }
}
