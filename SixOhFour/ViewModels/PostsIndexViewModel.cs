using curmudgeon.Utilities;
using SixOhFour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SixOhFour.ViewModels
{
    public class PostsIndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public Paginator Paginator { get; set; }
    }
}
