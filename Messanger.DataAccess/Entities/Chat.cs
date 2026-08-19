using Messanger.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messanger.DataAccess.Models
{
    public class Chat
    {

        public int Id { get; set; }

        public EChatType ChatType { get; set; } 

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public User Admin { get; set; } = null;

    }
}
