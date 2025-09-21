using DataAccounts.Entitys.MovieEntitys;
using DataAccounts.Entitys;
using DataAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    public class RootJson
    {
        public List<User> Users { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
