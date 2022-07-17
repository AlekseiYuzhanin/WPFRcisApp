using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.Models
{
    public class Connection
    {
        private static UserTaskBdContext _context;

        public static UserTaskBdContext GetContext()
        {
            if(_context == null)
                _context = new UserTaskBdContext();
            return _context;
        }

        public static User userAuth = new User();
    }
}
