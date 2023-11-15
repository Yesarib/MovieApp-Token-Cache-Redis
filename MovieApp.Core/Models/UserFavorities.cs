using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Models
{
    public class UserFavorities
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
    }
}
