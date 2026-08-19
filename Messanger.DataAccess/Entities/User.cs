using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Identity;

namespace Messanger.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; } 

        public string Name { get; set; } = string.Empty;

        public string OutputName { get; set; } = string.Empty;

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime RegDate { get; set; }

    }
}
