using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Configuration
{
    public class Client
    {
        public string Id { get; set; }
        public string ClientSecret { get; set; }
        public List<string> Audiences { get; set; }
    }
}
