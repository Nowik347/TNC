using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNC.WPF.Event.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Number { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
