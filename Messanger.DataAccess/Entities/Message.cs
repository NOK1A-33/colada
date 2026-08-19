using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messanger.DataAccess.Models
{
    public class Message
    {
        public int Id { get; set; }

        public Chat MChat { get; set; }
         
        public int AuthorId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
