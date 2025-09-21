using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccounts.Entitys.GenreEntitys
{
    public record GetGenresListModel(int pageNumber = 1, int pageSize = 10);
}
